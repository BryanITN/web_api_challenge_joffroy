using System;

namespace web_api_challenge.Dtos.Tasks
{
    public class DtoTaskView:DtoBaseTask
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StatusTaskDescription { get; set; }
    }
}
