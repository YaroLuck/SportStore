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
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                //GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl }); // в результате вывода метода браузеру клиента отправляется HTTP-инструкция пренаправления, которая сообщает брауеру запросить 
            //новый URL. В этом случае мы сообщаем браузеру запросить URL который будет вызывать метод действия Index контроллера Cart.
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        //виджет корзины
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        [HttpPost] // означает что метод Checkout будет вызван для обработки ПОСТ запроса. МЕХАНИЗМ СВЯЗЫВАНИЯ ДАННЫХ ТУТ ПРИСУТСТВУЕТ
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!"); //если нет товаров в корзине
            }
            if(ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        //добавление информации о заказе
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        //стр. 220
       /* private Cart GetCart()
        {
            // объект Session использует cookie или перезапись URL для группировки запросов от пользователя
            Cart cart = (Cart)Session["Cart"]; //чтобы извлечь объект мы считываем состояние сессии
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart; // добавление объекта в состояние сессии
            }
            return cart;
        }*/
    }
}