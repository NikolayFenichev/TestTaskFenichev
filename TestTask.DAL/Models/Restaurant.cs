namespace TestTask.DAL.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
