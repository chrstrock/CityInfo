using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;
[Route("/api/cities/{cityId:int}/pointsofinterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
    // GET
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        return city == null ?  NotFound() : Ok(city.PointsOfInterest);
    }

    [HttpGet("{pointOfInterestId:int}")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        var pointOfInterest = city?.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        return pointOfInterest == null || city == null ? NotFound() : Ok(pointOfInterest);
        
    }

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        PointOfInterestForCreationDto pointOfInterestForCreationDto)
    {
        return NotFound();
    }
    
}
