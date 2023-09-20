using EtaLearning.API.Models;

public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Clients.AddRange(
                       new Clients { Id = 1, Name = "Lustitia Ltd", CreationDate = DateTime.UtcNow },
                       new Clients { Id = 2, Name = "Bachmann", CreationDate = DateTime.UtcNow }
                   );
            dbContext.SaveChanges();
        }
    }
}