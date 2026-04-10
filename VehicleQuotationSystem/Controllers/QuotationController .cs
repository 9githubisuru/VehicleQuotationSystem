using Microsoft.AspNetCore.Mvc;
using QuotationAPI.Services;

namespace QuotationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly ExcelService _service = new ExcelService();

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null)
                return BadRequest("No file uploaded");

            var result = _service.ReadExcel(file);

            return Ok(result);
        }
    }
}