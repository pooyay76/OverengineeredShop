namespace Media_API.Contracts
{
    public class UploadMediaCommand
    {
        public IFormFile File { get; set; }
        public string Section { get; set; }

    }
}
