using AdminDyanamoEnterprises.IRepository;
using AdminDyanamoEnterprises.Repository;

using AdminDyanamoEnterprises.Repository;
using AdminDyanamoEnterprises.Repository;
using AdminDyanamoEnterprises.Repository.IRepository;

using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using static AdminDyanamoEnterprises.Repository.IMasterRepository;
using static AdminDyanamoEnterprises.Repository.IMasterRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHomeRepository, HomeRepository>();
builder.Services.AddTransient<IMasterRepository, MasterRepository>();
builder.Services.AddTransient<IColorRepository, MasterRepository>();
builder.Services.AddTransient<IFabricRepository, MasterRepository>();
builder.Services.AddTransient<IMaterialRepository, MasterRepository>();
builder.Services.AddTransient<IBlogsRepository, BlogsRepository>();


builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IBannerRepository, BannerRepository>();

builder.Services.AddTransient<IAccountRepository, AccountRepository>();




builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 30;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
}
);


//I am Narayan 

//I am Arif
var app = builder.Build();
//I am BASHAt
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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.UseNotyf();
app.Run();
