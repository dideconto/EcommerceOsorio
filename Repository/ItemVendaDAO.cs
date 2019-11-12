using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ItemVendaDAO : IRepository<ItemVenda>
    {
        private readonly Context _context;
        public ItemVendaDAO(Context context)
        {
            _context = context;
        }

        public bool Cadastrar(ItemVenda i)
        {
            _context.ItensVenda.Add(i);
            _context.SaveChanges();
            return true;
        }

        public List<ItemVenda> BuscarItensPorCarrinhoId(string carrinhoId)
        {
            return _context.ItensVenda.
                Include(x => x.Produto.Categoria).
                Where(x => x.CarrinhoId.Equals(carrinhoId)).
                ToList();
        }

        public List<ItemVenda> ListarTodos()
        {
            return _context.ItensVenda.ToList();
        }
        public ItemVenda BuscarPorId(int id)
        {
            return _context.ItensVenda.Find(id);
        }
    }
}
