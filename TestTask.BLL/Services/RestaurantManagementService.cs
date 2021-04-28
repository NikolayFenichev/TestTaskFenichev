using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.BLL.Services.Interfaces;
using TestTask.Common;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;

namespace TestTask.BLL.Services
{
    public class RestaurantManagementService : IRestaurantManagementService
    {
        IUnitOfWork _database;

        public RestaurantManagementService(IUnitOfWork uow)
        {
            _database = uow;
        }

        public async Task<CityDto> AddCityAsync(CityDto cityDto)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<CityDto, City>()));
            var city = mapper.Map<City>(cityDto);

            city = await _database.Cities.AddAsync(city);

            mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<City, CityDto>()));
            cityDto = mapper.Map<CityDto>(city);

            return cityDto;
        }

        public async Task<RestaurantDto> AddRestaurantAsync(RestaurantDto restaurantDto)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => 
                cfg.CreateMap<RestaurantDto, Restaurant>()));
            var restaurant = mapper.Map<Restaurant>(restaurantDto);

            restaurant = await _database.Restaurants.AddAsync(restaurant);

            mapper = new Mapper(new MapperConfiguration(cfg => 
                cfg.CreateMap<Restaurant, RestaurantDto>()));
            restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public async Task<PagedList<RestaurantDto>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId)
        {
            var restaurants = await _database.Restaurants.GetRestaurantsByCityAsync(pageParameters, cityId);

            var restaurantsDto = new PagedList<RestaurantDto>(restaurants.Select(r =>
                new RestaurantDto { Id = r.Id, Name = r.Name, CityId = r.CityId }).ToList(), 
                restaurants.TotalCount, restaurants.CurrentPage, restaurants.PageSize);

            return restaurantsDto;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
