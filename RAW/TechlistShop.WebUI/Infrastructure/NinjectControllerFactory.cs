using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using TechlistShop.Domain.Entities;
using Moq;
using TechlistShop.Domain.Concrete;
using System.Configuration;
using TechlistShop.Domain.Abstract;

namespace TechlistShop.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel nk; //Ninject Kernel

        public NinjectControllerFactory()
        {
            nk = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)nk.Get(controllerType);
        }

        private void AddBindings()
        {
            nk.Bind<IProductRepository>().To<EFProductRepository>();
            nk.Bind<ICarouselImageRepository>().To<EFCarouselImageRepository>();
            nk.Bind<IDiscountCarouselImageRepository>().To<EFDiscountCarouselImageRepository>();
            nk.Bind<IAddRepository>().To<EFAddRepository>();
            nk.Bind<ISettingRepository>().To<EFSettingRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            nk.Bind<IOrderProcessor>().To<EFOrderProccessor>().WithConstructorArgument("settings", emailSettings);

        }
    }
}