namespace ApiPrimeraPracticaAzureMMT.Helpers
{
    public class HelperPathProvider
    {
        private IWebHostEnvironment host;

        public HelperPathProvider(IWebHostEnvironment host)
        {
            this.host = host;
        }

        public string MapPath(string archivo)
        {
            string carpeta = "images";
            string root = host.WebRootPath;
            string path = Path.Combine(root, carpeta, archivo);
            return path;
        }

        public string MapUrlPath(string archivo)
        {
            string carpeta = "images";
            string urlPath = carpeta + "/" + archivo;
            return urlPath;
        }
    }
}
