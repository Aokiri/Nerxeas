using Microsoft.EntityFrameworkCore;
using Nerxeas.DataAccess;
using Nerxeas.DataAccess.Repository;
using Nerxeas.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add EntityFrameworkCore (DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity to EFCore. Commented the RequireConfirmedAccount for testing development reasons.
builder.Services.AddDefaultIdentity<IdentityUser>(/* options => options.SignIn.RequireConfirmedAccount = true */).AddEntityFrameworkStores<ApplicationDbContext>();

// Adding Dependency Injection for UnitOfWork with a Scoped lifetime.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
app.UseAuthentication(); // Added Authentication just before Authorization, to check if an user/passwd is valid.
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
