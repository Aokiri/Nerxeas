using Microsoft.EntityFrameworkCore;
using Nerxeas.DataAccess;
using Nerxeas.DataAccess.Repository;
using Nerxeas.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Nerxeas.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add EntityFrameworkCore (DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity to EFCore. Commented the RequireConfirmedAccount for testing development reasons.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(/* options => options.SignIn.RequireConfirmedAccount = true */)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
// Add routing to map Identity default options (added "/Identity/" to the route)
// 120. Always add ConfigureApplicationCookie after your Identity (AddIdentity)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Add Razor Pages (for Identity Purpouses)
builder.Services.AddRazorPages();

// Add Dependency Injection for UnitOfWork with a Scoped lifetime.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add an Scoped lifetime service for EmailSender
builder.Services.AddScoped<IEmailSender, EmailSender>();

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

app.MapRazorPages(); // Map Razor Pages for identity purpouses.
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
