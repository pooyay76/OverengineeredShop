using Media_API.Contracts;
using Media_API.Data;
using Media_API.Dtos;
using Media_API.Models;
using Media_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Media_API.Controllers;

[ApiController]
[Route("[controller]")]
public class MediaController : ControllerBase
{
    private readonly IFileService fileService;
    private readonly MediaDbContext _context;
    private readonly IWebHostEnvironment webHostEnvironment;
    public MediaController(IFileService fileService, MediaDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        this.fileService = fileService;
        _context = context;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("GetMedia/{id}", Name = "GetMedia")]
    public async Task<IActionResult> GetAsync(long id)
    {
        // Find the media record in the database by id
        var media = await _context.Medias.FirstOrDefaultAsync(m => m.Id == id);

        if (media == null)
        {
            return NotFound("File not found.");
        }
        var result = new MediaDto
        {
            Extension = media.Extension,
            Id = media.Id,
            Path = media.Path,
            Size = media.Size
        };
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFileAsync(long id)
    {
        // Find the media record in the database by id
        var media = await _context.Medias.FirstOrDefaultAsync(m => m.Id == id);

        if (media == null)
        {
            return NotFound("File not found.");
        }


        var path = Path.Combine(webHostEnvironment.WebRootPath, media.Path);

        // Ensure the file exists at the stored file path
        if (!System.IO.File.Exists(path))
        {
            return NotFound("File not found on server.");
        }

        // Return the file as a response
        var fileBytes = await System.IO.File.ReadAllBytesAsync(path); // or File.OpenRead(media.FilePath)
        var fileName = Path.GetFileName(media.Path);
        // Return the file with its MIME type
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        var mimeType = GetMimeType(fileExtension); // You can use a helper method to determine MIME type

        return File(fileBytes, mimeType, fileName);
    }

    [HttpPost(Name = "Upload")]
    public async Task<IActionResult> UploadAsync([FromForm] UploadMediaCommand command)
    {
        var path = await fileService.SaveFileAsync(command.File, command.Section);
        var media = new Media
        {
            Path = path,
            Extension = Path.GetExtension(path),
            Size = command.File.Length,
            Section = command.Section
        };

        await _context.Medias.AddAsync(media);
        await _context.SaveChangesAsync();
        return Ok(media.Id);
    }
    private string GetMimeType(string fileExtension)
    {
        return fileExtension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".mp4" => "video/mp4",
            // Add more types as necessary
            _ => "application/octet-stream" // Default binary type
        };
    }
}
