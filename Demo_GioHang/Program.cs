using Demo_GioHang.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GioHangDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//khai báo d?ch v? cho session
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(15); // Khai báo kho?ng th?i gian ?? Session timeout
    //có ngh?a là n?u ng??i dùng k th?c hi?n yêu c?u nào trong vòng 15s thì session c?a h? s? h?t h?n
    //n?u có thì b? ??m s? reset , d? li?u ???c l?u vào webserver

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // S? d?ng Session

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
