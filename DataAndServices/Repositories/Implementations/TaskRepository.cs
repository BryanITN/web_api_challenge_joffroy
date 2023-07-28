using web_api_challenge.Models;
using web_api_challenge.Repositories.Interfaces;

namespace web_api_challenge.Repositories.Implementations
{
   public class TaskRepository:GenericRepository<Task>,ITaskRepository
    {
        public TaskRepository(JoffroyChallengeContext context):base(context)
        {

        }
    }
}
