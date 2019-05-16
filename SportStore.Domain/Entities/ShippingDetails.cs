using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//для валидации
using System.ComponentModel.DataAnnotations;

namespace SportStore.Domain.Entities
{
    //класс для предоставления клиенту полей для ввода информации о доставке
    class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage  = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please")]
    }
}
