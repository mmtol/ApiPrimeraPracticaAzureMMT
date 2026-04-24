using ApiPrimeraPracticaAzureMMT.Helpers;
using ApiPrimeraPracticaAzureMMT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrimeraPracticaAzureMMT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingFilesController : ControllerBase
    {
        private HelperPathProvider helper;

        public TestingFilesController(HelperPathProvider helper)
        {
            this.helper = helper;
        }

        [HttpGet("Images")]
        public ActionResult<List<Models.FileInfo>> TestingFiles()
        {
            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            var files = Directory.GetFiles(carpeta)
                .Select(file => new Models.FileInfo
                {
                    fileName = Path.GetFileName(file),
                    urlpath = $"{Request.Scheme}://{Request.Host}/images/{Path.GetFileName(file)}"
                }).ToList();

            return Ok(files);
        }

        [HttpPost]
        public async Task<ActionResult<string>> TestingFiles(FileModel model)
        {
            byte[] bytes = Convert.FromBase64String(model.filecontent);
            string ruta = await SubirFileAsync(bytes, model.filename);
            return ruta;
        }

        private async Task<string> SubirFileAsync(byte[] bytes, string nombreArchivo)
        {
            string ruta = helper.MapPath(nombreArchivo);
            string url = helper.MapUrlPath(nombreArchivo);

            using (Stream stream = new FileStream(ruta, FileMode.Create))
            {
                await stream.WriteAsync(bytes, 0, bytes.Length);
            }

            return url;
        }
    }
}
