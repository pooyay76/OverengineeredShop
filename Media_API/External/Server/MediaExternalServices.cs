using Grpc.Core;
using Media.Api.Data;
using Media.Api.Models;
using Media.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Media.Api.External.Server
{
    public class MediaExternalServices : MediaServices.MediaServicesBase
    {
        private readonly MediaDbContext mediaContext;
        private readonly IFileService fileService;
        public MediaExternalServices(MediaDbContext mediaContext, IFileService fileService)
        {
            this.mediaContext = mediaContext;
            this.fileService = fileService;
        }
        public override async Task<UploadMediaResponse> UploadMedia(UploadMediaRequest request, ServerCallContext context)
        {
            var path = await fileService.SaveFileAsync(request.Filetype, request.Data.ToByteArray(), request.FileSection);
            var media = new MediaEntity
            {
                Path = path,
                Extension = Path.GetExtension(path),
                Size = request.Data.Length,
                Section = request.FileSection
            };

            await mediaContext.Medias.AddAsync(media);
            await mediaContext.SaveChangesAsync();

            UploadMediaResponse response = new()
            {
                Id = media.Id,
                MediaPath = path
            };
            return response;
        }
        public override async Task<GetMediasResponse> GetMedias(GetMediasRequest request, ServerCallContext context)
        {
            List<MediaDto> medias = await mediaContext.Medias
                .Where(m => request.Ids.Contains(m.Id))
                .Select(m => new MediaDto
                {
                    Id = m.Id,
                    MediaPath = m.Path
                })
                .ToListAsync();
            GetMediasResponse response = new GetMediasResponse() 
            { 
            MediaList = { medias }
            };
            return response;
        }
    }
}
