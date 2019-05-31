using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.Domain.Concrete;
using System.Configuration;
using SportStore.WebUI.Infrastructure.Abstract;
using SportStore.WebUI.Infrastructure.Concrete;


namespace SportStore.WebUI.Infrastructure
{
    //реализация пользовательской фабрики контроллеров
    //наследуюясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            //создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //получение обьекта контроллера из контейнера
            //используя его тип
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //привязка к имитированному хранилищю
            /*
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
            }.AsQueryable());

            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
            */        
            //привязка к реальному хранилищю
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            //эта привязка сообщает Нинжект что мы хотим создавать экз класса EFProductRepository для обслуживания запросов к интерфейсу IProductRepository
            //конфигурирование контейнера
            //Использование Ninject Для содания экземпляров IOrderProscessor
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false") //так мы читаем значение этого свойства, которое дает на доступ в WEB.CONFIG,настройки прилоежние
            };
            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}