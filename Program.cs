using FluentValidation.AspNetCore;
using System.Reflection;
using VisitorWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<VisitorRepository>();
builder.Services.AddScoped<OrganizationRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<DropDownRepository>();
builder.Services.AddScoped<UserTypeRepository>();
builder.Services.AddScoped<DashboardRepository>();
builder.Services.AddScoped<ImageRepository>();
builder.Services.AddScoped<NotificationRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

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

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
