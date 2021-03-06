﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core.Models;
using ThermoBet.SQLServer.Models;

namespace ThermoBet.Data
{
    public class ThermoBetContext : DbContext
    {
        public DbSet<TournamentModel> Tournaments { get; set; }
        public DbSet<MarketModel> Markets { get; set; }
        public DbSet<SelectionModel> Selections { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BetModel> Bets { get; set; }
        public DbSet<TournamentUserOptinModel> TournamentUserOptins { get; set; }

        public DbSet<LoginHistoryModel> LoginHistories { get; set; }

        public DbSet<ConfigurationModel> Configurations { get; set; }



        public ThermoBetContext(DbContextOptions<ThermoBetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors().EnableSensitiveDataLogging();
        }
    }
}
