namespace TestTask.DAL.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var city = (City)obj;

            return Id == city.Id
                && Name == city.Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
