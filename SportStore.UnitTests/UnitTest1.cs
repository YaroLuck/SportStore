using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using SportStore.WebUI.HtmlHelpers;
using SportStore.WebUI.Models;

namespace SportStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);

            controller.PageSize = 3;
            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name,"P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

            //Мы вызываем свойство Модел в рзультате, чтобы получить последовательность IEnumarable<Product> которую мы генерировали в методе List. Затем мы можем проверить те ли это данные
            //Мы преобразовали последовательность в массив и проверили длину и значение отдельных элементов.
        }

        [TestMethod]
        //этот тест проверяте вывод вспомогательного метода используя значение литеральной строки которая содержит двойные кавычки
        public void Can_Generate_Page_Links()
        {
            //Arrange - определить ХТМЛ хелпер - нам нужно сделать это чтобы применить метод расширения.
            HtmlHelper myHelper = null;

            //Arrange - создание данных PagingInfo
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10 };

            //Arrange - установить делегат используя лямбда-выражение
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
            + @"<a class=""selected"" href=""Page2"">2</a>"
            + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        //тест проверяет что контроллер отправляет в представление правильную информацию о нумерации страниц
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
                new Product {ProductID = 6, Name = "P6"},
            }.AsQueryable());

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductListViewModel result = (ProductListViewModel) controller.List(null,2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
