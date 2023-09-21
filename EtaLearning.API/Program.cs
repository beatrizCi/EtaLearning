using EtaLearning.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDatabase"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Clients.AddRange(
               new Clients { Id = 1, Name = "Lustitia Ltd", CreationDate = DateTime.UtcNow },
               new Clients { Id = 2, Name = "Bachmann", CreationDate = DateTime.UtcNow }
           );
    dbContext.SaveChanges();
}

app.Run();
