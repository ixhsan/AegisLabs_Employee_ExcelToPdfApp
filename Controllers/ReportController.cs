using AegisLabs_Employee_ExcelToPdfApp.Models;
using AegisLabs_Employee_ExcelToPdfApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System.Text.Json;

namespace AegisLabs_Employee_ExcelToPdfApp.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IReportService _reportService;

        public ReportController(IEmployeeService employeeService, IReportService reportService)
        {
            _employeeService = employeeService;
            _reportService = reportService;
        }

        [HttpGet("excel")]
        public async Task<IActionResult> GenerateExcel()
        {
            var employees = await _employeeService.GetAllAsync();
            if (employees == null || employees.Count == 0)
                return BadRequest("No employees found");

            var excelData = await _reportService.GenerateExcelAsync(employees);

            return File(
                excelData,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Employees_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("pdf")]
        public async Task <IActionResult> GeneratePdf()
        {
            var employees = await _employeeService.GetAllAsync();
            if (employees == null || employees.Count == 0)
                return BadRequest("No employees found");

            try
            {
                return new ViewAsPdf("PdfReport", employees)
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