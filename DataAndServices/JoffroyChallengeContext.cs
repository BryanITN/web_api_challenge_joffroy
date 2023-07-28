using System.Configuration;
using System.Data.Entity;
using MySql.Data.Entity;
using web_api_challenge.Models;

namespace web_api_challenge
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
     public class JoffroyChallengeContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<StatusTask> StatusTasks { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public JoffroyChallengeContext() : base("Server=165.232.154.120;Port=3306;Database=joffroyChallenge;Uid=bryan;Pwd=Dev0201***")
        {
             Database.SetInitializer<JoffroyChallengeContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }

}
