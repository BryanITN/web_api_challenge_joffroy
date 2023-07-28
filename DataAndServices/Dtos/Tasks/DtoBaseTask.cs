namespace web_api_challenge.Dtos.Tasks
{
    public class DtoBaseTask
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int StatusTaskId { get; set; }
    }
}
