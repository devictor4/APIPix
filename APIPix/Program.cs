using APIPix.IoC;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddDI(builder.Configuration);

app.Run();
