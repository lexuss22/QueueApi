using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Queue.BLL.Services.Interfaces;
using Queue.DTO.Models;
using System.Globalization;
using System.Text.Json;

namespace Queue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExportController : ControllerBase
    {
        private readonly IImportExportServices _services;

        public ImportExportController(IImportExportServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран.");

            using var stream = file.OpenReadStream();
                       
            var result = await _services.ImportFromExcelAsync(stream);

            if (result.StartsWith("Ошибка"))
                return BadRequest(result);

            return Ok(result);
            
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Export()
        {
            try
            {
                var bytes = await _services.ExportToJsonAsync();
                return File(bytes, "application/json", "schedule.json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

