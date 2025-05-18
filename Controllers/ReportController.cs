using AegisLabs_Employee_ExcelToPdfApp.Models;
using AegisLabs_Employee_ExcelToPdfApp.Services;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace AegisLabs_Employee_ExcelToPdfApp.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private static List<Employee> _tempEmployees = new(); // Temporary data cache

        public ReportController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("setdata")]
        public IActionResult SetEmployees([FromBody] List<Employee> employees)
        {
            if (employees == null || employees.Count == 0)
                return BadRequest("No employees provided");

            _tempEmployees = employees;
            return Ok();
        }

        [HttpGet("excel")]
        public IActionResult GenerateExcel()
        {
            if (_tempEmployees.Count == 0)
                return BadRequest("No employees to generate report");

            var excelData = _employeeService.GenerateExcelAsync(_tempEmployees).Result;

            return File(
                excelData,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Employees_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("pdf")]
        public IActionResult GeneratePdf()
        {
            if (_tempEmployees.Count == 0)
                return BadRequest("No employees to generate report");

            try
            {
                return new ViewAsPdf("PdfReport", _tempEmployees)
                {
                    FileName = $"Employees_{DateTime.Now:yyyyMMdd}.pdf",
                    PageOrientation = Orientation.Portrait,
                    PageSize = Size.A4
                };
            }
            catch (Exception ex)
            {
                return BadRequest($"PDF generation error: {ex.Message}");
            }
        }

    }
}