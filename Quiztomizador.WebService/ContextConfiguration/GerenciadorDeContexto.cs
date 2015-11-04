using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.ContextConfiguration
{
    public class GerenciadorDeContexto
    {
        private static GerenciadorDeContexto _gerenciadorDeContexto = new GerenciadorDeContexto();
        private Context _context;

        private GerenciadorDeContexto() { }

        public static GerenciadorDeContexto GetInstance() 
        {
            return _gerenciadorDeContexto;
        }

        public Context GetContext() 
        {
            if (_context == null)
                _context = new Context();

            return _context;
        }

        public void DisposeContext() 
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}