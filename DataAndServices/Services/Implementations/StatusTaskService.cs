using System.Collections.Generic;
using System.Linq;
using web_api_challenge.Dtos.StatusTasks;
using web_api_challenge.Exceptions;
using web_api_challenge.Models;
using web_api_challenge.Repositories.Interfaces;
using web_api_challenge.Services.Interfaces;

namespace web_api_challenge.Services.Implementations
{
    public class StatusTaskService : IStatusTaskService
    {
        private readonly IStatusTaskRepository _statusTaskRepository;

        public StatusTaskService(IStatusTaskRepository statusTaskRepository)
        {
            _statusTaskRepository = statusTaskRepository;
        }

        public void CreateStatusTask(DtoStatusTask statusTask)
        {
            var existsStatus = _statusTaskRepository.GetByFilter(status => status.Description.Equals(statusTask.Description)).Count()>0;
            if (existsStatus)
                throw new JoffroyException("Ya existe un estatus con la misma descripción.");
            StatusTask newStatusTask = new StatusTask();
            newStatusTask.Description = statusTask.Description;
            _statusTaskRepository.Add(newStatusTask);
            _statusTaskRepository.Save();
            

        }

        public List<DtoStatusTaskView> GetAllStatusTask()
        {
            return _statusTaskRepository.GetAll().Select(status => new DtoStatusTaskView
            {
                Id = status.Id,
                Description = status.Description
            }).ToList();
        }

        public void UpdateStatusTask(DtoStatusTask statusTask, int statusId)
        {
            var updateStatusTask = _statusTaskRepository.GetById(statusId);
            if (updateStatusTask==null)
                throw new JoffroyException($"El estatus con id: {statusId} no existe.");
            updateStatusTask.Description = statusTask.Description;
            _statusTaskRepository.Update(updateStatusTask);
            _statusTaskRepository.Save();
        }
    }
}
