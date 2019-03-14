using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportStore.Domain.Entities;
using System.Web.Mvc;

namespace SportStore.WebUI.Binders
{
    //сообщаем мвс что она может использовать этот класс для создания экземпляров объекта Cart. Делать это нужно в методе Appliction_Start файла Global.asax 
    public class CartModelBinder : IModelBinder  // определяет один метод BindModel
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) //передаем два параметра для того чтобы сделать возможным создание объекта доменной модели.
        {
            //get the Cart from the session
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey]; //ControllerContext обеспечивает доступ ко всей информации которойрасполагает класс контроллера
            // и которая включает в себя детали запроса клиента.
            //прочитав значение ключа из данных сессии, мы получаем объект Cart или, если его не существует, содаем новый.
            //crate the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            //retur cart
            return cart;
        }
    }
}