using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStore.WebUI.Models
{
    //класс для передачи данных между представлением и контроллером. Для удобства.
    public class PagingInfo
    {
        public int TotalItems { get; set; } //всего обьектов
        public int ItemsPerPage { get; set; } //количество обьектов на странице
        public int CurrentPage { get; set; } //номер текущей страницы

        public int TotalPages //всего страниц
        {
            get { return (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage); }
        }
    }
}