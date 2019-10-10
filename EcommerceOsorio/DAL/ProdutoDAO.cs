using EcommerceOsorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceOsorio.DAL
{
    public class ProdutoDAO
    {
        private readonly Context _context;
        public ProdutoDAO(Context context)
        {
            _context = context;
        }
        public void Cadastrar(Produto p)
        {
            _context.Produtos.Add(p);
            _context.SaveChanges();
        }
    }
}
