using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SportStore.Domain.Abstract;
using SportStore.WebUI.Models;

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
        public ViewResult List(string category, int page = 1)
        {
            //свойства класса представления модели
            //public IEnumerable<Product> Products { get; set; }
            //public PagingInfo PagingInfo { get; set; }
            //public string CurrentCategory { get; set; }
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                                         .Where(p => p.Category == null || p.Category == category)
                                         .OrderBy(p => p.ProductID)
                                         .Skip((page - 1) * PageSize)
                                         .Take(PageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //TotalItems = repository.Products.Count()
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            };
            /*return View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));*/
            return View(model);
        }
    }
}