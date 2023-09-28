using EtaLearning.API.Data;
using EtaLearning.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EtaLearning.DataAccess
{
    public static class DependencyInjection
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
