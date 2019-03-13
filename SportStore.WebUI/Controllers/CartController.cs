using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        // GET: Cart
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl }); // в результате вывода метода браузеру клиента отправляется HTTP-инструкция пренаправления, которая сообщает брауеру запросить 
            //новый URL. В этом случае мы сообщаем браузеру запросить URL который будет вызывать метод действия Index контроллера Cart.
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            // объект Session использует cookie или перезапись URL для группировки запросов от пользователя
            Cart cart = (Cart)Session["Cart"]; //чтобы извлечь объект мы считываем состояние сессии
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart; // добавление объекта в состояние сессии
            }
            return cart;
        }
    }
}