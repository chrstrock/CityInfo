using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id:int}")]
    public ActionResult<CityDto> GetCity(int id)
    {
       var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

       return cityDto == null ? NotFound() : Ok(cityDto);
    }
    
    
}