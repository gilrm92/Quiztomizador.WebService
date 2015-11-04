using Ninject;
using Quiztomizador.WebService.Model.IRepositorios;
using Quiztomizador.WebService.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.NinjectConfiguration
{
    public class NinjectControllerFactory
    {
        public static IKernel Kernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            Kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            Kernel.Bind<IRepositorioUsuario>().To<RepositorioUsuario>();
            
        }

    }
}