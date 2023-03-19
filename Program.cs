using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); ;
builder.Services.AddDbContext<CarPoolingDbContext>(OptionsBuilder => OptionsBuilder.UseSqlServer("Server=localhost;Database=CarPoolingDataBase;Trusted_Connection=True;Trust Server Certificate=True;"));
//builder.Services.AddDbContext<CarPoolingDbContext>(OptionsBuilder => OptionsBuilder.UseSqlServer("Server=localhost;Database=CarPoolingDataBase;Trusted_Connection=True;Trust Server Certificate=True;MultipleActiveResultSets=true;"));
builder.Services.AddScoped<IOfferedRides, OfferedRides>();
builder.Services.AddScoped<ILoginCredentials, LoginCredentials>();
builder.Services.AddScoped<ICities, Cities>();
builder.Services.AddScoped<IStops, Stops>();
builder.Services.AddScoped<IBookRides, BookRides>();

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

app.Run();
