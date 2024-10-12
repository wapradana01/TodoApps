using Todo.Api.Apps.Extensions.StartupExtensions;
using Todo.Api.Core;
using Todo.Api.DataAccess;
using Todo.Api.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterShared(builder.Host, builder.Configuration);

//builder.Services.AddControllers();
builder.AddController();
builder.AddFluentValidation();
builder.AddHealthCheck();
builder.Services.RegisterDataAccess(builder.Configuration);
builder.Services.RegisterCore(builder.Configuration);
builder.AddUserManagement();
builder.WebHost.UseUrls("https://*:7244");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDbContext<ApplicationDbContext>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
