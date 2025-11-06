using Catalog.Api.External.Client;

namespace Catalog.Api.Contracts.Interfaces
{
    public interface IMediaClient
    {
        Task<List<MediaDto>> GetMediasAsync(List<long> ids);
        Task<MediaDto> UploadMediaAsync(string fileExtension, byte[] data);
    }
}
