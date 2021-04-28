using System;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.Common;

namespace TestTask.BLL.Services.Interfaces
{
    public interface IRestaurantManagementService: IDisposable
    {
        Task<CityDto> AddCityAsync(CityDto cityDto);
        Task<RestaurantDto> AddRestaurantAsync(RestaurantDto restaurantDto);
        Task<PagedList<RestaurantDto>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId);
    }
}
