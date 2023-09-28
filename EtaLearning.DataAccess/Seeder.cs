using EtaLearning.API.Data.Entities;
using EtaLearning.API.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EtaLearning.DataAccess
{
    public static class Seeder
    {
        public static void AddNewData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!dbContext.Clients.Any())
                {
                    dbContext.Clients.AddRange(
                           new Client { Name = "Lustitia Ltd", CreationDate = DateTime.UtcNow },
                           new Client { Name = "Bachmann", CreationDate = DateTime.UtcNow }
                       );
                    dbContext.SaveChanges();
                }

            }
        }
    }
}
