namespace APIProject6.WebAPI.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string IconBackground { get; set; } = "bg-primary";
        public DateTime NotificationDate { get; set; }
        public bool IsRead { get; set; }
    }
}
