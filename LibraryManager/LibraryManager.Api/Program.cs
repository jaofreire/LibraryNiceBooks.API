using DependencyInjection;
using LibraryManager.Api;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var stage = Configurations.DevelopmentStage;

builder.Services.AddConfigurations(builder.Configuration, stage);

builder.Services.AddCors(o =>
{
    o.AddPolicy(Configurations.PolicyName,
        policy => policy
        .WithOrigins([Configurations.FrontEndURL, Configurations.BackEndURL])
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Configurations.PolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
