using System.ComponentModel.DataAnnotations;

namespace TestTask.BLL.Dto
{
    public class CityDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя города
        /// </summary>
        [Required(ErrorMessage = "Не указано имя города")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var city = (CityDto)obj;

            return Id == city.Id
                && Name == city.Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
