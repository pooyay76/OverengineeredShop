namespace Media.Api.Contracts
{
    public class UploadMediaCommand
    {
        public IFormFile File { get; set; }
        public string Section { get; set; }

    }
}
