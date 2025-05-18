using AegisLabs_Employee_ExcelToPdfApp.Models;
using Microsoft.JSInterop;
using OfficeOpenXml;
using System.Text;
using System.Text.Json;

namespace AegisLabs_Employee_ExcelToPdfApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public EmployeeService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                var storageData = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "employees");

                if (string.IsNullOrEmpty(storageData))
                    return new List<Employee>();

                return JsonSerializer.Deserialize<List<Employee>>(storageData) ?? new List<Employee>();
            }
            catch
            {
                return new List<Employee>();
            }
        }

        public async Task SaveEmployeesAsync(List<Employee> employees)
        {
            var storageData = JsonSerializer.Serialize(employees);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "employees", storageData);
        }

        public async Task<byte[]> GenerateExcelAsync(List<Employee> employees)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Employees");

            // Headers
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Email";
            worksheet.Cells[1, 3].Value = "Phone";
            worksheet.Cells[1, 4].Value = "Address";

            // Style headers
            using (var range = worksheet.Cells[1, 1, 1, 4])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Data
            for (int i = 0; i < employees.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = employees[i].Name;
                worksheet.Cells[i + 2, 2].Value = employees[i].Email;
                worksheet.Cells[i + 2, 3].Value = employees[i].Phone;
                worksheet.Cells[i + 2, 4].Value = employees[i].Address;
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            // Return as byte array
            return await package.GetAsByteArrayAsync();
        }

        public async Task<byte[]> GeneratePdfAsync(List<Employee> employees)
        {
            // In a real application, you would use a PDF library here
            // For simplicity, we'll create an HTML string and convert to PDF using Rotativa

            var html = new StringBuilder();
            html.Append("<html><head><style>");
            html.Append("table { width: 100%; border-collapse: collapse; }");
            html.Append("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
            html.Append("th { background-color: #f2f2f2; }");
            html.Append("</style></head><body>");
            html.Append("<h1>Employee Report</h1>");
            html.Append("<table>");
            html.Append("<tr><th>Name</th><th>Email</th><th>Phone</th><th>Address</th></tr>");

            foreach (var employee in employees)
            {
                html.Append("<tr>");
                html.Append($"<td>{employee.Name}</td>");
                html.Append($"<td>{employee.Email}</td>");
                html.Append($"<td>{employee.Phone}</td>");
                html.Append($"<td>{employee.Address}</td>");
                html.Append("</tr>");
            }

            html.Append("</table></body></html>");

            // In a real implementation, you would convert this HTML to PDF
            // For now, we'll simulate by returning the bytes of HTML
            return Encoding.UTF8.GetBytes(html.ToString());
        }
    }
}