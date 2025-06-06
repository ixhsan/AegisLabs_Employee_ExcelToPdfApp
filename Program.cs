using AegisLabs_Employee_ExcelToPdfApp.Components;
using AegisLabs_Employee_ExcelToPdfApp.Repositories;
using AegisLabs_Employee_ExcelToPdfApp.Repositories.Interfaces;
using AegisLabs_Employee_ExcelToPdfApp.Services;
using AegisLabs_Employee_ExcelToPdfApp.Services.Interfaces;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IReportService, ReportService>();


builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7139/");
});

// Register services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

var rotativaPath = Path.Combine(Directory.GetCurrentDirectory(), "RotativaTool");
RotativaConfiguration.Setup(rotativaPath);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultControllerRoute();

app.Run();