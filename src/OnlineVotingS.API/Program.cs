using Microsoft.AspNetCore.Identity;
using OnlineVotingS.API.Middleware;
using OnlineVotingS.Application;
using OnlineVotingS.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  // Register MVC services
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger to include JWT Authentication support
//builder.Services.AddSwaggerGen();

builder.Services.ConfigureService(builder.Configuration);

// Call the new AddApplicationServices method from the Application layer
builder.Services.AddApplicationServices();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Configure Logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Voting API v1");
//        options.RoutePrefix = string.Empty;
//    });
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=GuestDashboard}/{id?}");

app.Run();
