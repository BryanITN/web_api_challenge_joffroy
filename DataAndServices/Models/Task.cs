using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_challenge.Models
{
   public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int StatusTaskId { get; set; }

        public StatusTask StatusTask { get; set; }

        public Task()
        {
        }
    }
}
