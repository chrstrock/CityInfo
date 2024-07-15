using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpGet("{pointOfInterestId:int}", Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        var pointOfInterest = city?.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        return pointOfInterest == null || city == null ? NotFound() : Ok(pointOfInterest);
        
    }

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,
        PointOfInterestForCreationDto pointOfInterestForCreationDto)
    {
        
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest)
            .Max(p => p.Id);

        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterestForCreationDto.Name,
            Description = pointOfInterestForCreationDto.Description
        };
        
        city.PointsOfInterest.Add(finalPointOfInterest);

        return CreatedAtRoute("GetPointOfInterest", new
        {
            cityId = cityId,
            pointOfInterestId = finalPointOfInterest.Id
        },
            finalPointOfInterest);
    }

    [HttpPut("{pointofinterestid}")]
    public ActionResult UpdatePointOfInterest(int cityId, int pointofinterestid,
        PointOfInterestForUpdateDto pointOfInterest)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if(city == null)
        {
            return NotFound();
        }
        
        //find point of interest
        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestid);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }
        
        pointOfInterestFromStore.Name = pointOfInterest.Name;
        pointOfInterestFromStore.Description = pointOfInterest.Description;

        return NoContent();

    }

    [HttpPatch("{pointofinterestid}")]
    public ActionResult PartiallyUpdatedPointOfInterest(int cityId, int pointofinterestid,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        //find point of interest
        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestid);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description
        };

        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(pointOfInterestToPatch))
        {
            return BadRequest(ModelState);
        }

        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

        return NoContent();
    }

    [HttpDelete("{pointOfInterestId:int}")]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        var pointOfInterestFromStore = city.PointsOfInterest
            .FirstOrDefault(c => c.Id == pointOfInterestId);

        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        city.PointsOfInterest.Remove(pointOfInterestFromStore);
        return NoContent();
    }
    

}
