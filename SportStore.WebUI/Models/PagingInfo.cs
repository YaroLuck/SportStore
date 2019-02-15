using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStore.WebUI.Models
{
    //класс для передачи данных между представлением и контроллером. Для удобства.
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage); }
        }
    }
}