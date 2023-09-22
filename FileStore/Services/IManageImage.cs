namespace FileStore.Services
{
    public interface IManageImage
    {
        Task<string> UploadFile(IFormFile _IFormFile);
        Task<(byte[],string,string)> DownloadFile(string FileName);
        bool DeleteFile(string FileName);
    }
}
