using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_challenge.Models
{
   public class User
    {

        public int id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Task> Tasks { get; set; }



        public User()
        { 
        }

    }

}
