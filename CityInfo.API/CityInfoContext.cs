using CityInfo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API;

public class CityInfoContext : DbContext
{
    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
    }

    public DbSet<CityDto> Cities { get; set; } = null!;
    public DbSet<PointOfInterestDto> PointsOfInterest { get; set; } = null!;
}