namespace PhotoSharingWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
