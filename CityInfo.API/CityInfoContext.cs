using CityInfo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API;

public class CityInfoContext : DbContext
{
    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;
}