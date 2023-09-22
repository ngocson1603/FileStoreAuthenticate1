using FileStore.Helper;
using Microsoft.AspNetCore.StaticFiles;

namespace FileStore.Services
{
    public class ManageImage : IManageImage
    {
        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = Common.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath,out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllByteAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllByteAsync, _ContentType,Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> UploadFile(IFormFile _IFormFile)
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = _IFormFile.FileName+"_"+DateTime.Now.Ticks.ToString()+_FileInfo.Extension;
                var _GetFilePath = Common.GetFilePath(FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }
                return FileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteFile(string FileName)
        {
            try
            {
                var filePath = Common.GetFilePath(FileName);

                // Kiểm tra xem tệp tồn tại trước khi xóa
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true; // Xóa thành công
                }
                else
                {
                    return false; // Tệp không tồn tại
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                throw ex;
            }
        }
    }
}
