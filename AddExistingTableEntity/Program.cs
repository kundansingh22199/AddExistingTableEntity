using AddExistingTableEntity.Models;
using DataAccess;
using DataAccess.Models;
using InterfaceLibrary;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add your DbContext (SscapitalContext) and connection string
builder.Services.AddDbContext<SscapitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));

// Register ProcedureService
//builder.Services.AddScoped<ProcedureService>();
builder.Services.AddTransient<IContract, DataAccessLayer>();
builder.Services.AddTransient<ISscapitalContext, SscapitalContext>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
