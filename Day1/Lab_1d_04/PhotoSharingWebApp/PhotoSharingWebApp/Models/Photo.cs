using System;

namespace PhotoSharingWebApp.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] File { get; set; }
        public DateTime CreationDate { get; set; }
        public int OwnerId { get; set; }
    }
}
