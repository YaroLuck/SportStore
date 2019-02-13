using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        //добавляем конструктор который принимает параметр IProductrepository
        //это позволит Ninject внедрять зависимость для хранилища товаров когда он будет содавать экзкласса контроллера.
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        //List() - метод действия, визуализирующий представление, показывающее полный список товаров.
        public ActionResult List()
        {
            return View(repository.Products);
        }
    }
}