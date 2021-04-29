using System.ComponentModel.DataAnnotations;

namespace TestTask.BLL.Dto
{
    public class RestaurantDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// Имя ресторана
        /// </summary>
        [Required(ErrorMessage = "Не указано имя ресторана")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор города
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Идентификартор города должен быть больше 0")]
        public int CityId { get; set; }

        public override bool Equals(object obj)
        {
            var restaurant = (RestaurantDto)obj;

            return Id == restaurant.Id
                && Name == restaurant.Name
                && CityId == restaurant.CityId;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
                ^ Name.GetHashCode()
                ^ CityId.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Id = {0}, Name = {1}, CityId = {2}", Id, Name, CityId);
        }
    }
}
