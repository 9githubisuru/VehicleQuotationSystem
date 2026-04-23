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


            //return Ok(result);
            var data = _service.ReadExcel(file);

            //✅ SAVE TO DB
            _repository.SaveQuotation(data.Vehicles);

            return Ok(data);

        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using QuotationAPI.Models;
//using QuotationAPI.Repositories;
//using QuotationAPI.Services;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//namespace QuotationAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuotationController : ControllerBase
//    {
//        private readonly ExcelService _service = new ExcelService();
//        private readonly QuotationRepository _repository;

//        //private readonly QuotationRepository _repository = new QuotationRepository();

//        public QuotationRepository Repository => _repository;


//        [HttpPost("upload")]
//        public IActionResult Upload(
//            IFormFile file       
//        )
//        {
//            if (file == null)
//                return BadRequest("No file uploaded");

//            //// ✅ Deserialize form inputs
//            //var request = JsonConvert.DeserializeObject<Vehicle>(formData);

//            // ✅ Read Excel
//            var result = _service.ReadExcel(file);

//            Repository.SaveQuotation(result.Vehicles);


//            // ✅ Merge FORM DATA → EACH VEHICLE
//            //foreach (var v in result.Vehicles)
//            //{
//            //    v.FDPERIOD = request.FDPERIOD;
//            //    v.FDPERIODID = request.FDPERIODID;
//            //    v.FDYEAR = request.FDYEAR;
//            //    v.FDISPOLFEE = request.FDISPOLFEE;
//            //    v.ISINCLTAX = request.ISINCLTAX;
//            //    v.FDPERMENTVEH = request.FDPERMENTVEH;
//            //    v.FDSINGVEHI = request.FDSINGVEHI;
//            //    v.ISNEWDUPQUOT = request.ISNEWDUPQUOT;
//            //}

//            return Ok(new
//            {
//                message = "Quotation saved successfully",
//                vehicles = result.Vehicles.Count
//            });
//        }
//    }
//}