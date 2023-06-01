namespace ParkhausKorte.DbService;

using System;
using ParkhausKorte.DbService;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using Microsoft.EntityFrameworkCore;

public class ParkingGarageContext : DbContext
{
    public ParkingGarageContext(DbContextOptions<ParkingGarageContext> options):base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingGarageEntity>()
            .HasNoKey();
    }
    //entities
    public DbSet<ParkingGarageEntity> ParkingGarage { get; set; }
    public DbSet<ParkerEntity> Parkers { get; set; }
    public DbSet<ParkingSpotEntity> ParkingSpots { get; set; }
}