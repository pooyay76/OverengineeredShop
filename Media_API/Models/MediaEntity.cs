namespace Media.Api.Models
{
    public class MediaEntity
    {
        public long Id { get; private set; }
        public string Section { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public DateTime UploadedAt { get; private init; } = DateTime.UtcNow;
        public long Size { get; set; }
    }

}
