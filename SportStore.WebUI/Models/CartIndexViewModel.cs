using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportStore.Domain.Entities;

namespace SportStore.WebUI.Models
{
    //простой класс модели представления
    public class CartIndexViewModel  //нужно передать две порции информации в представление которое будет отображать содержимое корзины.
        //объект cart и URL который будет отображен если пользователь нажмет кнопку Continue shoppig. 
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}