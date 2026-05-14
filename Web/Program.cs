using BLL.Services;
using DAL.EF;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

// Repos
builder.Services.AddScoped<AuthRepo>();
builder.Services.AddScoped<RoleRepo>();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleService>();

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

app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
