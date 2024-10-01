using Bookify.Web.Seeds;
using Bookify.Web.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Serilog;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// we make a class containing all services configration in section extensions have a class called dependency injection 
builder.Services.AddBookifyServices(builder);// check plz extensions section , class dependency injection 


// if i need to remove "[antiforgerytoken]" from all controllers in anu action "post put delete patch" expect "post"  i can add it here in services
// here we add [antiforgerytoken] to all controllers in all actions except get , if i have any action 
//i need it do't has [antiforgerytoken] i will use [ignoreantiForgeryToken] in action
builder.services.AddMvc(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            );

//Add Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

//custom  middleware for closing my application in iframe in any html code 
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "Deny");

    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}"); // for error page different statecode except 404  and 500 see more in doc
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// if i  need to make all cookies maked as Secure use this middleware  
//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    Secure = CookieSecurePolicy.Always
//});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//seeding the roles and adimin account for the first time to make migration to database 

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using var scope = scopeFactory.CreateScope();

var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

await DefaultRoles.SeedAsync(roleManger);
await DefaultUsers.SeedAdminUserAsync(userManger);

//hangfire

//if you use hangfire without security filter 

//app.UseHangfireDashboard("/hangfire");  // any one can acess and modify in dashboard so we need more security 

// use hangfire dashboard for AdminsOnly
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Bookify Dashboard", //title optional
    IsReadOnlyFunc = (DashboardContext context) => true,// here you can set the dashboard to read only not for anyone make modifications
    Authorization = new IDashboardAuthorizationFilter[]// we have interface called IDashboardAuthorizationFilter that has implemented un filter section 
    {
        new HangfireAuthorizationFilter("AdminsOnly") // go to filter section for see implementation
        //  go to up in configration class  to add

        //  services.Configure<AuthorizationOptions>(options =>
        //     options.AddPolicy("AdminsOnly", policy =>
        //     {
        //         policy.RequireAuthenticatedUser();
        //         policy.RequireRole(AppRoles.Admin);
        //     }));
    }

});

// you need to check first task section to see implementation of this class
// this for get service in one class that when we start application it will work with
// when we make jobs like send emails or whatsupp in controller it need to call controller so we make that 

// we use "scope" to get services here in program.cs 

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var webHostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
var whatsAppClient = scope.ServiceProvider.GetRequiredService<IWhatsAppClient>();
var emailBodyBuilder = scope.ServiceProvider.GetRequiredService<IEmailBodyBuilder>();
var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

var hangfireTasks = new HangfireTasks(dbContext, webHostEnvironment, whatsAppClient,
    emailBodyBuilder, emailSender);

RecurringJob.AddOrUpdate(() => hangfireTasks.PrepareExpirationAlert(), "0 14 * * *");
RecurringJob.AddOrUpdate(() => hangfireTasks.RentalsExpirationAlert(), "0 14 * * *");





// this is for sending userid and uername in log error "need to see serilog section in configration appsettings.json"

//  {
//         "Name": "MSSqlServer",
//         "Args": {
//           "connectionString": "Server=(localdb)\\mssqllocaldb;Database=Bookify;Trusted_Connection=True;MultipleActiveResultSets=true",
//           "tableName": "ErrorLogs",
//           "schemaName": "logging",
//           "autoCreateSqlTable": true,
//           "ColumnOptionsSection": {
//             //"removeStandardColumns": [ "MessageTemplate" ],
//             "customColumns": [
//               {
//                 "ColumnName": "UserId",
//                 "DataType": "nvarchar",
//                 "DataLength": "450"
//               },
//               {
//                 "ColumnName": "UserName",
//                 "DataType": "nvarchar",
//                 "DataLength": "256"
//               }
//             ]
//           }
//         },
//         "restrictedToMinimumLevel": "Error"
//       }
app.Use(async (context, next) =>
{
    LogContext.PushProperty("UserId", context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    LogContext.PushProperty("UserName", context.User.FindFirst(ClaimTypes.Name)?.Value);

    await next();
});

app.UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
