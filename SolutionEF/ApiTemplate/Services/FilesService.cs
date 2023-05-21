using ApiTemplate.Services.Interfaces;

namespace ApiTemplate.Services
{
    public class FilesService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FilesService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string SaveFile(IFormFile document, string directory, string directorySecond)
        {
            try
            {
                var routeBase = Path.Combine(_env.WebRootPath, directory);
                if (directorySecond != null && directorySecond.Length > 0)
                {
                    routeBase = Path.Combine(routeBase, directorySecond);
                }

                if (!Directory.Exists(routeBase))
                {
                    Directory.CreateDirectory(routeBase);
                }
                var nameDoc = $"{Guid.NewGuid()}{Path.GetExtension(document.FileName)}";                
                var pathroot = Path.Combine(routeBase, nameDoc);

                using (var stream = System.IO.File.Create(pathroot))
                {
                    document.OpenReadStream().CopyToAsync(stream);
                }

                var pathDoc = directory + "/" + directorySecond + "/" + nameDoc;

                return pathDoc;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool UpdateFile(IFormFile file, string directory, string beforeImage, string? directorySecond)
        {

            try
            {
                var pathroot = Path.Combine(_env.WebRootPath, directory);
                if(directorySecond != null && directorySecond.Length > 0)
                {
                    pathroot = Path.Combine(pathroot, directorySecond);
                }

                string namefile = Path.GetFileName(beforeImage);

                if (!Directory.Exists(pathroot))
                {
                    Directory.CreateDirectory(pathroot);
                }

                if(!DeleteFile(directory, beforeImage, directorySecond))
                    return false;

                pathroot = Path.Combine(pathroot, namefile);

                using (var stream = System.IO.File.Create(pathroot))
                {
                    file.OpenReadStream().CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFile(string directory, string beforeImage, string? directorySecond)
        {
            try
            {
                var pathroot = Path.Combine(_env.WebRootPath, directory);
                if (directorySecond != null && directorySecond.Length > 0)
                {
                    pathroot = Path.Combine(pathroot, directorySecond);
                }

                if (beforeImage != null)
                {
                    var beforepath = Path.Combine(pathroot, Path.GetFileName(beforeImage));
                    if (File.Exists(beforepath))
                    {
                        System.IO.File.Delete(beforepath);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
