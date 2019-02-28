using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // роуты обрабатываются в том порядке в котором они указаны
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Выводит список товаров из всех категорий для первой страницы
            routes.MapRoute(
                null,
                "",
                new
                {
                    controller = "Products",
                    action = "List",
                    categoty = (string) null,
                    page = 1
                }
            );
            //Выводит список товаров из всех категорий для указанной страницы (в данном слуае страницы 2)
            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+"}
            );
            //Показывает первую страницу товаров из определенной категории(в данном случае Soccer)
            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Product", action = "List", page = 1}
            );
            //Показывает указанную страницу (в данном случае 2) товаров из указанной категории (в данном случае Soccer)
            routes.MapRoute(
                name: null,
                url: "{controller}/Page{page}",
                defaults: new { controller = "Product", action = "List" },
                new { page = @"\d+" }
            );
            //вызывает метод действия Else контроллера Anything
            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
