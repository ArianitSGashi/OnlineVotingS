using Microsoft.AspNetCore.Identity;
using OnlineVotingS.API.Middleware;
using OnlineVotingS.API.Validations;
using OnlineVotingS.Application;
using OnlineVotingS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  // Register MVC services
builder.Services.AddEndpointsApiExplorer();

// Configure application services
builder.Services.ConfigureService(builder.Configuration);

// Call the new AddApplicationServices method from the Application layer
builder.Services.AddApplicationServices();

builder.Services.AddScoped<IElectionValidation, ElectionValidation>();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireVoterRole", policy => policy.RequireRole("Voter"));
});

// Configure logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
    options.Cookie.SameSite = SameSiteMode.Lax;  
    options.Cookie.HttpOnly = true; 
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Set to desired expiration time
    options.SlidingExpiration = true;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  
    options.Cookie.SameSite = SameSiteMode.Lax;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Use custom global exception handling middleware
app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=GuestDashboard}/{id?}");

app.Run();
