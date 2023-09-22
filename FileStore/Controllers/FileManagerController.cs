using FileStore.Helper;
using FileStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FileManagerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile _IFormFile)
        {
            var result = await _unitOfWork.ManageImage.UploadFile(_IFormFile);
            return Ok(result);
        }
        [HttpGet]
        [Route("downloadfile")]
        public async Task<IActionResult> DownloadFile(string FileName)
        {
            var result = await _unitOfWork.ManageImage.DownloadFile(FileName);
            return File(result.Item1,result.Item2,result.Item2);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return BadRequest("File name must be provided.");
                }

                // Gọi hàm DeleteFile để xóa tệp
                var result = _unitOfWork.ManageImage.DeleteFile(fileName);

                if (result)
                {
                    return Ok($"File {fileName} deleted successfully.");
                }
                else
                {
                    return NotFound($"File {fileName} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the file.");
            }
        }
    }
}
