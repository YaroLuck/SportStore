using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SportStore.Domain.Abstract;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public int PageSize = 4;
        //добавляем конструктор который принимает параметр IProductrepository
        //это позволит Ninject внедрять зависимость для хранилища товаров когда он будет содавать экзкласса контроллера.
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        //List() - метод действия, визуализирующий представление, показывающее полный список товаров.
        public ViewResult List(int page = 1)
        {
            
            return View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}