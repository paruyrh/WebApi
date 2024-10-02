using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace AuthProject.Controllers
{
    [ApiController]
    public class PdfController(IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        /// <summary>
        /// Download pdf.
        /// </summary>
        /// <returns>
        /// <see cref="ActionResult"/>
        /// </returns>
        [HttpGet("menu/{name}")]
        public ActionResult DownloadPDF(string name)
        {
            string rootPath = _webHostEnvironment.ContentRootPath;
            string outputFilePath = Path.Combine(rootPath, name);

            if (!System.IO.File.Exists(name))
            {
                // Return a 404 Not Found error if the file does not exist
                return NotFound();
            }
            var pdf = System.IO.File.ReadAllBytes(outputFilePath);

            Response.ContentType = MediaTypeNames.Application.Pdf;
            Response.Headers.Append(HeaderNames.ContentDisposition, "inline");
            Response.Headers.Append(HeaderNames.Connection, "Upgrade, Keep-Alive");
            Response.Headers.Append(HeaderNames.KeepAlive, "timeout=5, max=1000");
            if (pdf != null)
                return File(pdf, "application/pdf");
            else
                return BadRequest("result.Message");
        }
    }
}
