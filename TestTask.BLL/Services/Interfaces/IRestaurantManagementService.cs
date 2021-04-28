using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.Common;
using TestTask.DAL.Models;

namespace TestTask.BLL.Services.Interfaces
{
    public interface IRestaurantManagementService: IDisposable
    {
        Task<CityDto> AddCityAsync(CityDto cityDto);
        Task<RestaurantDto> AddRestaurantAsync(RestaurantDto restaurantDto);
        Task<PagedList<RestaurantDto>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId);
    }
}
