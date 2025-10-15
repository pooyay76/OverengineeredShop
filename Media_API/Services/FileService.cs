namespace Media_API.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subFolder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException("No file uploaded.");
            }

            string path = Path.Combine(webHostEnvironment.WebRootPath, subFolder);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var extension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";
            // Generate a unique filename to avoid conflicts
            var filePath = Path.Combine(path, newFileName);

            // Ensure the file doesn't already exist (optional)

            // Save the file to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Path.Combine(subFolder, newFileName); ;
        }
    }
}
