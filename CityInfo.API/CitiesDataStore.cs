using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public CitiesDataStore()
    {
        Cities = new List<CityDto>()
        {
            new()
            {
                Id = 1,
                Name = "New York City",
                Description = "The one with that big park.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new (){
                    Id = 1,
                    Name = "Central Park",
                    Description = "The most visited urban park in the United States.",
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Empire State Building",
                        Description = "The most famous skyscraper in the world."
                    }
                }
            },
            new()
            {
                Id = 2,
                Name = "Antwerp",
                Description = "The one with the cathedral that was never really finished.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new()
                    {
                        Id = 3,
                        Name = "Antwerp Central Station",
                        Description = "The the finest example of railway architecture in Belgium."
                    },
                    new()
                    {
                        Id = 4,
                        Name = "Cathedral of Our Lady",
                        Description = "A gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                    }
                    
                }
            },
            
            new()
            {
                Id = 3,
                Name = "Paris",
                Description = "The one with that big tower.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new()
                    {
                        Id = 5,
                        Name = "Eiffel Tower",
                        Description = "Tower built by Gustave Eiffel."
                    },
                    new()
                    {
                        Id = 6,
                        Name = "The Louvre",
                        Description = "The world's largest museum."
                        
                    }
                }
            }
        };

    }

    public List<CityDto> Cities { get; set; }
    
    public static CitiesDataStore Current { get; } = new();
    
    //init dummy data
}