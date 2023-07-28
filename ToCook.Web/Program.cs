using Microsoft.EntityFrameworkCore;
using ToCook.Core.Services.Interfaces;
using ToCook.Core.Services;
using ToCook.DataLayer.Context;
using ToCook.Core.Convertors;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region DbContext

var connectionString = builder.Configuration.GetConnectionString("ToCookConnectionString") ?? throw new InvalidOperationException(" ConnectionString 'DakeKhakiConnectionString' Not Found. ");
builder.Services.AddDbContext<ToCookContext>(options =>
options.UseSqlServer(connectionString)
) ;

#endregion

#region IoC

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Loguot";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

#endregion

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
