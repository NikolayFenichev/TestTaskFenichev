using System;

namespace TestTask.DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private RestaurantManagementContext _db;
        private ICityRepository _cityRepository;
        private IRestaurantRepository _restaurantRepository;

        private bool disposed = false;

        public ICityRepository Cities
        {
            get
            {
                if (_cityRepository == null)
                    _cityRepository = new CityRepository(_db);

                return _cityRepository;
            }
        }

        public IRestaurantRepository Restaurants
        {
            get
            {
                if (_restaurantRepository == null)
                    _restaurantRepository = new RestaurantRepository(_db);

                return _restaurantRepository;
            }
        }

        public UnitOfWork()
        {
            _db = new RestaurantManagementContext();
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
