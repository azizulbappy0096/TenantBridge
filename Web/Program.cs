using BLL.Services;
using DAL.EF;
using DAL.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication("auth").AddCookie(opt =>
{
    opt.LoginPath = "/Auth/Login";
    opt.LogoutPath = "/Auth/Logout";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    opt.SlidingExpiration = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

// Repos
builder.Services.AddScoped<AuthRepo>();
builder.Services.AddScoped<RoleRepo>();
builder.Services.AddScoped<PropertyRepo>();
builder.Services.AddScoped<LeaseRepo>();
builder.Services.AddScoped<PaymentRepo>();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<LeaseService>();
builder.Services.AddScoped<PaymentService>();

// Database Context
builder.Services.AddDbContext<TenantBridgeContext>(opt => { 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")); 
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
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}")
    .WithStaticAssets();

//app.MapControllers();

app.Run();
