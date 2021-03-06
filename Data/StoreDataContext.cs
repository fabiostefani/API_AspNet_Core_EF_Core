﻿using fabiostefani.io.MapaCatalog.Api.Data.Maps;
using fabiostefani.io.MapaCatalog.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fabiostefani.io.MapaCatalog.Api.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(@"Host = localhost; Port = 5432; Pooling = true; Database = MapaCatalog; User Id = postgres; Password = Postgres2019!; ");            
            optionsBuilder.UseNpgsql(Settings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            //base.OnModelCreating(modelBuilder);
        }
    }
}
