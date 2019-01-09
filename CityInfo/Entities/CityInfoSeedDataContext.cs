using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Entities
{
    public static class CityInfoSeedDataContext
    {
        public static void SeedDataToCityInfoContext(this CityInfoContext context) {
            if (context.Cities.Any())
            {
                return;
            }

            var cities = new List<City>() {
                new City(){
                    Name ="New York",
                    Description ="The one with the big park",
                    PointsOfInterest = new List<PointsOfInterest>(){
                        new PointsOfInterest(){
                            Name ="Central park",
                            Description ="The most visit urban park visit in the USA"
                        },
                        new PointsOfInterest(){
                            Name ="Empire state building",
                            Description ="A 102-story skyscraper"
                        }
                    }
                },
                new City(){ 
                    Name ="Antwerp", Description="The one with a cathedral that was never really finished",
                    PointsOfInterest = new List<PointsOfInterest>(){
                        new PointsOfInterest(){
                            Name ="Central park",
                            Description ="The most visit urban park visit in the USA"
                        },
                        new PointsOfInterest(){
                            Name ="Empire state building",
                            Description ="A 102-story skyscraper"
                        }
                    }
                },
                new City(){
                    Name ="Paris", Description="The one with the big tower",
                    PointsOfInterest = new List<PointsOfInterest>(){
                        new PointsOfInterest(){
                            Name ="Central park",
                            Description ="The most visit urban park visit in the USA"
                        },
                        new PointsOfInterest(){
                            Name ="Empire state building",
                            Description ="A 102-story skyscraper"
                        }
                    }
                },
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
