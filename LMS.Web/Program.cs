using LMS.Web.Services;
using LMS.Web.Services.IServices;
using LMS.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add HttpClient, HttpContext
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// add HttpClient for Services
builder.Services.AddHttpClient<IBaseService, BaseService>();    
builder.Services.AddHttpClient<IGroupService, GroupService>();    
builder.Services.AddHttpClient<IStudentService, StudentService>();    
builder.Services.AddHttpClient<ISubjectService, SubjectService>();    

// add service lifetime
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

// SD Urls populated
SD.GroupAPIBase = builder.Configuration["ServiceUrls:GroupAPI"];
SD.StudentAPIBase = builder.Configuration["ServiceUrls:StudentAPI"];
SD.SubjectAPIBase = builder.Configuration["ServiceUrls:SubjectAPI"];

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
