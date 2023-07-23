using Microsoft.EntityFrameworkCore;
using ToCook.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region DbContext

var connectionString = builder.Configuration.GetConnectionString("ToCookConnectionString") ?? throw new InvalidOperationException(" ConnectionString 'DakeKhakiConnectionString' Not Found. ");
builder.Services.AddDbContext<ToCookContext>(options =>
options.UseSqlServer(connectionString)
) ;

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
