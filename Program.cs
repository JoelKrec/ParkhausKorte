using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ParkhausKorte.Data;
using ParkhausKorte.DbService;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
registerServices(builder.Services);

var app = builder.Build();

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


static void registerServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddScoped<ParkingGarage>();

    // Connect to database.
    var connectionString = "server=172.17.0.1;port=49153;user=db_user;password=db_password;database=parkhaus";
    var serverVersion = ServerVersion.AutoDetect(connectionString);//new MariaDbServerVersion(new Version(10, 10, 3));

    services.AddDbContext<ParkingGarageContext>(
        dbContextOptions => dbContextOptions
            .UseMySql(connectionString, serverVersion)
            // The following three options help with debugging, but should
            // be changed or removed for production.
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
    );
}
