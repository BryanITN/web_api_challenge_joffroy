using System;
using System.Collections.Generic;
using System.Linq;
using web_api_challenge.Dtos.Tasks;
using web_api_challenge.Exceptions;
using web_api_challenge.Models;
using web_api_challenge.Repositories.Interfaces;
using web_api_challenge.Services.Interfaces;

namespace web_api_challenge.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStatusTaskRepository _statusTaskRepository;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IStatusTaskRepository statusTaskRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _statusTaskRepository = statusTaskRepository;
        }

        public void CreateTask(DtoCreateUpdateTask task)
        {

            ValidateFields(task);
            var existsDuplicateTitleTask = _taskRepository.GetByFilter(t => t.Title.Equals(task.Title) && t.UserId == task.UserId);
            if (existsDuplicateTitleTask.ToList().Count > 0)
                throw new JoffroyException("Ya se encuentra registrada una tarea con el mismo titulo, debes colocar otro titulo");
            Task newTask = new Task();
            newTask.Title = task.Title;
            newTask.Description = task.Description;
            newTask.CreatedDate = task.CreatedDate;
            newTask.UserId = task.UserId;
            newTask.StatusTaskId = task.StatusTaskId;
            _taskRepository.Add(newTask);
            _taskRepository.Save();

        }

        public void DeleteTask(int taskId)
        {
           var task= ValidateExistence(taskId);
            _taskRepository.Delete(task);
            _taskRepository.Save();
        }

        public List<DtoTaskView> GetAllTaskByUser(int userId)
        {
            var existsUser = _userRepository.GetById(userId) != null;
            if (!existsUser)
                throw new JoffroyException("El usuario no existe.");
            var result = _taskRepository.GetAllIncluding(t => t.User, t => t.StatusTask).Where(t => t.UserId == userId).Select(t => new DtoTaskView
            {
                Title=t.Title,
                Description=t.Description,
                UserId=t.UserId,
                StatusTaskId=t.StatusTaskId,
                CreatedDate=t.CreatedDate,
                StatusTaskDescription=t.StatusTask.Description,
                Id=t.Id
            }).OrderBy(t=>t.CreatedDate).ToList();

            return result;
        }

        public DtoResumeTask GetResumeTaskByUser(int userId)
        {
            var existsUser = _userRepository.GetById(userId) != null;
            if (!existsUser)
                throw new JoffroyException("El usuario no existe.");
            int quantityPending = _taskRepository.GetByFilter(task => task.UserId == userId && task.StatusTaskId == 1).Count();
            int quantityFinished = _taskRepository.GetByFilter(task => task.UserId == userId && task.StatusTaskId == 2).Count();
            var result = new DtoResumeTask()
            {
                QuantityPendingToDo = quantityPending,
                QuantityFinished = quantityFinished
            };
            return result;
        }

        public DtoTaskView GetTaskById(int taskId)
        {
            ValidateExistence(taskId);
            DtoTaskView result = null;
            try
            {
                 result = _taskRepository.GetAllIncluding(t => t.StatusTask).Where(t => t.Id == taskId).Select(task => new DtoTaskView()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    CreatedDate = task.CreatedDate,
                    UserId = task.UserId,
                    StatusTaskId = task.StatusTaskId,
                    StatusTaskDescription = task.StatusTask.Description
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new JoffroyException(ex.Message);
            }
            return result;
        }

        public void UpdateTask(DtoCreateUpdateTask task, int taskId)
        {
           var updateTask= ValidateExistence(taskId);
            ValidateFields(task);
            var existsDuplicateTitleTask = _taskRepository.GetByFilter(t => t.Title.Equals(task.Title) && t.UserId == task.UserId&&t.Id!=taskId);
            if (existsDuplicateTitleTask.ToList().Count > 0)
                throw new JoffroyException("Ya se encuentra registrada una tarea con el mismo titulo, debes colocar otro titulo");
            updateTask.Title = task.Title;
            updateTask.Description = task.Description;
            updateTask.StatusTaskId = task.StatusTaskId;
            _taskRepository.Update(updateTask);
            _taskRepository.Save();
        }

        Task ValidateExistence(int taskId)
        {
            var task = _taskRepository.GetById(taskId);
            if (task == null)
                throw new JoffroyException($"La tarea con id: {taskId} no existe.");
            return task;
        }

        void ValidateFields(DtoCreateUpdateTask task)
        {
            var existsStatusTask = _statusTaskRepository.GetById(task.StatusTaskId) != null;
            if (!existsStatusTask)
                throw new JoffroyException("El estatus asignado a la tarea no es valido");
            var existsUser = _userRepository.GetById(task.UserId) != null;
            if (!existsUser)
                throw new JoffroyException($"El usuario asignado a la tarea no es valido.");
           
        }
    }
}
