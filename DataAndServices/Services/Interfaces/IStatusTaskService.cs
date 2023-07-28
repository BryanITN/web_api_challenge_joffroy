using System.Collections.Generic;
using web_api_challenge.Dtos.StatusTasks;

namespace web_api_challenge.Services.Interfaces
{
   public interface IStatusTaskService
    {
        void CreateStatusTask(DtoStatusTask statusTask);
        void UpdateStatusTask(DtoStatusTask statusTask, int statusId);
        List<DtoStatusTaskView> GetAllStatusTask();
    }
}
