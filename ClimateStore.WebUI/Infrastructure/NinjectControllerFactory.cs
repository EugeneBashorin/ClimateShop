﻿using ClimateStore.Domain.Abstract;
using ClimateStore.Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClimateStore.Domain.Concrete;
using System.Configuration;
using ClimateStore.WebUI.Infrastructure.Abstract;
using ClimateStore.WebUI.Infrastructure.Concrete;


namespace ClimateStore.WebUI.Infrastructure
{
    // реализация пользовательской фабрики контроллеров,наследуясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            // создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // получение объекта контроллера из контейнера используя его тип
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // конфигурирование контейнера
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> {
            //    new Product { Name = "Ensa Cr500T Wh", Price = 2300},
            //    new Product { Name = "Teplov Y2000", Price = 3200},
            //    new Product { Name = "Water Filter NEOS 101P", Price = 4100}
            //}.AsQueryable()); //AsQueryable метод расширения LINQ, преобразует IEnumerable<T> в IQueryable<T>

            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);

            // замена привязки Ninject к имитированному хранилищу на привязку к реальному хранилищу
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            //Bind Authenticate
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}
