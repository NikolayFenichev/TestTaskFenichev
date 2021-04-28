using System.Collections.Generic;
using TestTask.BLL.Dto;
using TestTask.Common;
using TestTask.DAL.Models;

namespace TestTask.Tests
{
    class Common
    {
        public const string CityName = "Хабаровск";
        public const string RestaurantName = "Ресторан";
        public const int TotalCount = 7;
        public const int PageSize = 3;
        public const int PageNumber = 2;
        public const int CityId = 1;
        public const int BadCityId = 0;

        public readonly List<RestaurantDto> RestaurantsDto = new List<RestaurantDto>()
        {
            new RestaurantDto
            {
                Id = 1,
                Name = "У Реки",
                CityId = 1
            },
            new RestaurantDto
            {
                Id = 2,
                Name = "Амо",
                CityId = 2
            },
            new RestaurantDto
            {
                Id = 3,
                Name = "Ribs",
                CityId = 3
            },
            new RestaurantDto
            {
                Id = 4,
                Name = "Джани",
                CityId = 4
            },
            new RestaurantDto
            {
                Id = 5,
                Name = "Джорджиано",
                CityId = 5
            },
            new RestaurantDto
            {
                Id = 6,
                Name = "Квеври",
                CityId = 6
            },
            new RestaurantDto
            {
                Id = 7,
                Name = "Пяткин",
                CityId = 7
            }
        };
        public readonly PagedList<Restaurant> Restaurants = new PagedList<Restaurant>()
        {
            new Restaurant
            {
                Id = 1,
                Name = "У Реки",
                CityId = 1
            },
            new Restaurant
            {
                Id = 2,
                Name = "Амо",
                CityId = 2
            },
            new Restaurant
            {
                Id = 3,
                Name = "Ribs",
                CityId = 3
            },
            new Restaurant
            {
                Id = 4,
                Name = "Джани",
                CityId = 4
            },
            new Restaurant
            {
                Id = 5,
                Name = "Джорджиано",
                CityId = 5
            },
            new Restaurant
            {
                Id = 6,
                Name = "Квеври",
                CityId = 6
            },
            new Restaurant
            {
                Id = 7,
                Name = "Пяткин",
                CityId = 7
            }
        };
    }
}
