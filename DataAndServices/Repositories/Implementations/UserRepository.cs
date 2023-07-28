using MySql.Data.Entity;
using System.Data.Entity;
using web_api_challenge.Models;
using web_api_challenge.Repositories.Interfaces;

namespace web_api_challenge.Repositories.Implementations
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(JoffroyChallengeContext context):base(context)
        {

        }
    }
}
