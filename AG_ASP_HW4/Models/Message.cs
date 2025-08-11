namespace AP_project.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public string? MessageText { get; set; }
        public DateTime MessageDate { get; set; }
    }
}