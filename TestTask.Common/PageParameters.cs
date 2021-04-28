using System.ComponentModel.DataAnnotations;

namespace TestTask.Common
{
    public class PageParameters
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 10;

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
