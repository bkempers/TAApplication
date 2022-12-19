/**
 * Author:      Trevor Koenig
 * Partner:     Ben Kempers
 * Date:        9/27/2022
 * Course:      CS 4540, University of Utah, School of Computing
 * Copyright:   CS 4540 and [Ben Kempers and Trevor Koenig] - This work may not be copied for use in Academic Coursework.
 *
 * I, Trevor Koenig and Ben Kempers, certify that I wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 *
 *    Main entry point into the program, initializes the database and views
 *    
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TAApplication.Areas.Data;
using TAApplication.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebPWrecover.Services;
using TAApplication.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TAContext");
builder.Services.AddDbContext<TAContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<TAUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TAContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddAuthentication()
    .AddGoogle(options =>
                 {
        IConfigurationSection googleAuthNSection =
        builder.Configuration.GetSection("Authentication:Google");

        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
    });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var DB = scope.ServiceProvider.GetRequiredService<TAContext>();
    var um = scope.ServiceProvider.GetRequiredService<UserManager<TAUser>>();
    var rm = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await DB.InitializeUsers(um, rm);
    await DB.InitializeApplications(um);
    await DB.InitializeCourses(um);
    await DB.InitializeAvailability(um);
    await DB.InitializeEnrollments("wwwroot/csv/temp.csv", um);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
