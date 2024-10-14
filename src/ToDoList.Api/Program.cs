using Swashbuckle.AspNetCore.SwaggerGen;
using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.Persistence.ToDoListDb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomOperationIds(apiDescription =>
        apiDescription.TryGetMethodInfo(out var methodInfo)
            ? methodInfo.Name
            : null);
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddToDoListDb(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(x => x.DisplayOperationId());

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
