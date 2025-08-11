namespace AP_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Pwd { get; set; }
        public string? Salt { get; set; }
    }
}