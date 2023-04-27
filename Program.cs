using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ParkhausKorte.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

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

int aP = 0;//alleParker
int fP = 0;//freieparkplätze
int fDP = 0;//freieDauerParkplätze
int kP = 0;//kurzparker
int dP = 0;//dauerparker
const int mP = 180 - 4;//MaximaleParkplätze
const int rDP = 40;//reservierteDauerparkplätze

if(dP > rDP)
{
    fP = mP - aP;
}
else
{
    fP = mP - 40 - kP;
}

fDP = mP - aP;