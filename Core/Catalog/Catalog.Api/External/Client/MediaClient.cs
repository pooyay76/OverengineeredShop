
using Catalog.Api.Contracts;
using Google.Protobuf;

namespace Catalog.Api.External.Client
{
    public class MediaClient : IMediaClient
    {
        private readonly MediaServices.MediaServicesClient client;

        public MediaClient(MediaServices.MediaServicesClient client)
        {
            this.client = client;
        }
        public async Task<MediaDto> UploadMediaAsync(string fileExtension,byte[] data)
        {
            var result = await client.UploadMediaAsync(new UploadMediaRequest
            {
               Data = ByteString.CopyFrom(data),
               FileSection = @"catalog\products",
               Filetype = fileExtension
            });
            var urlPath = result.MediaPath.Replace(@"\", "/");
            var response = new MediaDto
            {
                Id = result.Id,
                MediaPath = urlPath
            };
            return response;
        }
        public async Task<List<MediaDto>> GetMediasAsync(List<long> ids)
        {
            var response = await client.GetMediasAsync(new GetMediasRequest
            {
                Ids = {ids}
            });

            return response.MediaList.ToList();
        }
    }
}
