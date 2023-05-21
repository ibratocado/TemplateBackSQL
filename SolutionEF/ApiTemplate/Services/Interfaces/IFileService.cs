namespace ApiTemplate.Services.Interfaces
{
    public interface IFileService
    {
        string SaveFile(IFormFile document, string directory, string directorySecond = "");
        bool UpdateFile(IFormFile file, string directory, string beforeImage, string directorySecond = "");
        bool DeleteFile(string directory, string beforeImage, string directorySecond = "");
    }
}
