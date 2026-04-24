using Microsoft.AspNetCore.Mvc;
using QuotationAPI.Repositories;
using QuotationAPI.Services;

namespace QuotationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly ExcelService _service = new ExcelService();


        private readonly QuotationRepository _repository;

        public QuotationController(QuotationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null)
                return BadRequest("No file uploaded");


            //read Excel
            var data = _service.ReadExcel(file);

            //SAVE TO DB
            _repository.SaveQuotation(data.Vehicles);

            return Ok(data);

        }
    }
}

