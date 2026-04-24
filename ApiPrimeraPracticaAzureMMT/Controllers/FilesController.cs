using ApiPrimeraPracticaAzureMMT.Helpers;
using ApiPrimeraPracticaAzureMMT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrimeraPracticaAzureMMT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private HelperPathProvider helper;

        public FilesController(HelperPathProvider helper)
        {
            this.helper = helper;
        }

        [HttpGet]
        [Route("[action]/Images")]
        public ActionResult<List<Models.FileInfo>> TestingFiles()
        {
            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            var files = Directory.GetFiles(carpeta)
                .Select(file => new Models.FileInfo
                {
                    FileName = Path.GetFileName(file),
                    UrlPath = $"{Request.Scheme}://{Request.Host}/images/{Path.GetFileName(file)}"
                }).ToList();

            return Ok(files);
        }

        [HttpPost]
        public async Task<ActionResult<string>> TestingFiles(FileModel model)
        {
            byte[] bytes = Convert.FromBase64String(model.FileContent);
            string ruta = await SubirFileAsync(bytes, model.FileName);
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
