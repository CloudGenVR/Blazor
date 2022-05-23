using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [Consumes("multipart/form-data")]
        [HttpPost("single")]
        public async Task<IActionResult> UploadSingle(IFormFile file)
        {
            var name = file.FileName;
            var size = file.Length;
            return Ok(new
            {
                FileName = name,
                Size = size,
            });
        }


    }
}
