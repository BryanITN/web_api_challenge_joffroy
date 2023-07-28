using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_challenge.Models
{
   public class StatusTask
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
        public StatusTask()
        {

        }
    }
}
