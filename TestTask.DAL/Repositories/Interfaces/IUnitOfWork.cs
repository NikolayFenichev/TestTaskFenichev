using System;

namespace TestTask.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IRestaurantRepository Restaurants { get; }
    }
}
