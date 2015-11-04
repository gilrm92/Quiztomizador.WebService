using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.NinjectConfiguration
{
    public class GetServiceHelper
    {
        public static T GetService<T>()
        {
            return (T)NinjectControllerFactory.Kernel.GetService(typeof(T));
        }
    }
}