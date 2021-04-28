namespace TestTask.DAL.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }

        public override bool Equals(object obj)
        {
            var restaurant = (Restaurant)obj;

            return Id == restaurant.Id
                && Name == restaurant.Name
                && CityId == restaurant.CityId
                && City.Equals(restaurant.City);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
