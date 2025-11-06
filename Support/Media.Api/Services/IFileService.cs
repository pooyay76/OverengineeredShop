using Microsoft.AspNetCore.Mvc.Formatters;

namespace Media.Api.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string subFolder);
        Task<string> SaveFileAsync(string fileType, byte[] data, string subFolder);
    }
}
