using web_api_challenge.Models;
using web_api_challenge.Repositories.Interfaces;

namespace web_api_challenge.Repositories.Implementations
{
   public class StatusTaskRepository:GenericRepository<StatusTask>,IStatusTaskRepository
    {
        public StatusTaskRepository(JoffroyChallengeContext context):base(context)
        {

        }
    }
}

