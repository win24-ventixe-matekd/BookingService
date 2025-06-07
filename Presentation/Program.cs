using Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase")));

var app = builder.Build();

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.RoutePrefix = string.Empty;
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Service Api");
});

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();