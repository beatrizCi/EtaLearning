using EtaLearning.API.Models;

public class Startup
{

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Clients.Add(new Clients { Name = "Initial Client" });
            dbContext.SaveChanges();
        }

    }
}