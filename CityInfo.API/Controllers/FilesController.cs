using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
    
    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                                            ?? throw new System.ArgumentNullException(
                                                nameof(fileExtensionContentTypeProvider));
    }
    // GET
    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        const string pathToFile = "documentTest.pdf";
        
        //check if the file exists
        if(!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }
        
        if(!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
        {
            contentType = "application/octet-stream";
            
        }

        var bytes = System.IO.File.ReadAllBytes(pathToFile);
        
        return File(bytes, contentType, Path.GetFileName(pathToFile));

    }

    [HttpPost]
    public async Task<ActionResult> CreateFile(IFormFile file)
    {
        if (file.Length == 0 || file.Length > 20971520 || file.ContentType != "application/pdf")
        {
            return BadRequest("No file or an invalid one has been inputted.");
        }
        
        //Create the file path.  Only okay for demo practices

        var path = Path.Combine(Directory.GetCurrentDirectory(), $"uploaded_file_{Guid.NewGuid()}.pdf");

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok("Your file has been uploaded successfully");

    }
}