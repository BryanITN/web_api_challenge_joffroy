using System.Collections.Generic;
using web_api_challenge.Dtos.Tasks;

namespace web_api_challenge.Services.Interfaces
{
    public interface ITaskService
    {
        void CreateTask(DtoCreateUpdateTask task);
        void UpdateTask(DtoCreateUpdateTask task,int taskId);
        void DeleteTask(int taskId);
        List<DtoTaskView> GetAllTaskByUser(int userId);
        DtoTaskView GetTaskById(int taskId);
        DtoResumeTask GetResumeTaskByUser(int userId);
    }
}
