using Microsoft.AspNetCore.Mvc;
using QuotationAPI.Data;

namespace QuotationAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public TestController(OracleDbContext context)
        {
            _context = context;
        }

        [HttpGet("db")]
        public IActionResult TestConnection()
        {
            try
            {
                using var conn = _context.GetConnection();
                conn.Open();

                return Ok("✅ Oracle Connection Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "❌ Connection Failed",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}