using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportStore.Domain.Entities;


namespace SportStore.WebUI.Models
{
    //класс представления модели
    public class ProductListViewModel
    {
        //свойства класса представления модели
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}