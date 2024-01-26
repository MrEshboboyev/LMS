using AutoMapper;
using LMS.Services.SubjectAPI;
using LMS.Services.SubjectAPI.Data;
using LMS.Services.SubjectAPI.Services;
using LMS.Services.SubjectAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// add Sql Connection
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// configuring AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// added services lifetime
builder.Services.AddScoped<ISharedService, SharedService>();
builder.Services.AddScoped<IGroupService, GroupService>();

// add connection : SubjectAPI and SharedAPI
builder.Services.AddHttpClient("Shared", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:SharedAPI"]));
// add connection : SubjectAPI and GroupAPI
builder.Services.AddHttpClient("Group", u => u.BaseAddress =
new Uri(builder.Configuration["ServiceUrls:GroupAPI"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (db.Database.GetPendingMigrations().Count() > 0)
        {
            db.Database.Migrate();
        }
    }
}
