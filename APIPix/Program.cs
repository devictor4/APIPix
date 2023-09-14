using APIPix.IoC;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDI(builder.Configuration);

var app = builder.Build();
{
    app.UseRouting();
    app.UseAuthorization();
    app.MapRazorPages();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();

    //if (app.Environment.IsDevelopment())
    //{
        
    //}
}

app.Run();