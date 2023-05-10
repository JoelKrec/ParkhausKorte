namespace ParkhausKorte.DbService;

using System;
using ParkhausKorte.DbService;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using Microsoft.EntityFrameworkCore;

public class ParkingGarageContext : DbContext
{
    public ParkingGarageContext(DbContextOptions<ParkingGarageContext> options):base(options) {}

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingGarageEntity>()
            .HasNoKey();

        /*modelBuilder.Entity<ParkerEntity>()
            .Property(u => u.numberPlate).IsOptional();*/
    }
    //entities
    public DbSet<ParkingGarageEntity> parkinggarage { get; set; }
    public DbSet<ParkerEntity> parkers { get; set; }
}