using Employee_Skill_Inventory.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
var sqlConnectionConfiguration = new SqlConnectionConfiguration(builder.Configuration.GetConnectionString("SqlDbContext"));
builder.Services.AddSingleton(sqlConnectionConfiguration);
builder.Services.AddServerSideBlazor(o => o.DetailedErrors = true);
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF1cXGNCd0x0RHxbf1x0ZFNMYlxbRndPMyBoS35RckVrW31edndWQ2NZVkN3");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
