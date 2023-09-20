﻿using Microsoft.EntityFrameworkCore;

namespace EtaLearning.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clients> Clients { get; set; }
    }
}
