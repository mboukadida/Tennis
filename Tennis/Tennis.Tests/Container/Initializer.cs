using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Tests.Container
{
   public static class Initializer
    {
        public static IWindsorContainer RegisterTypes()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());

            return container;
        }
    }
}
