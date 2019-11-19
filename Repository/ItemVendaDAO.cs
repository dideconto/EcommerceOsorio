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
        public ItemVenda BuscarPorId(int id)
        {
            return _context.ItensVenda.Find(id);
        }
        public bool Cadastrar(ItemVenda i)
        {
            ItemVenda itemAux = _context.ItensVenda.
                FirstOrDefault(x => x.Produto.ProdutoId == i.Produto.ProdutoId &&
                x.CarrinhoId.Equals(i.CarrinhoId));
            if (itemAux == null)
            {
                _context.ItensVenda.Add(i);
            }
            else
            {
                itemAux.Quantidade++;
            }
            _context.SaveChanges();
            return true;
        }
        public List<ItemVenda> ListarTodos()
        {
            return _context.ItensVenda.ToList();
        }
        public List<ItemVenda> ListarItensPorCarrinhoId(string carrinhoId)
        {
            return _context.ItensVenda.
                Include(x => x.Produto.Categoria).
                Where(x => x.CarrinhoId.Equals(carrinhoId)).
                ToList();
        }

        public double RetornarTotalCarrinho(string carrinhoId)
        {
            return _context.ItensVenda.
                Where(x => x.CarrinhoId.Equals(carrinhoId)).
                Sum(x => x.Quantidade * x.Preco);
        }

        public void Remover(int id)
        {
            _context.ItensVenda.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
        public void Alterar(ItemVenda i)
        {
            _context.ItensVenda.Update(i);
            _context.SaveChanges();
        }

        public void AumentarQuantidade(int id)
        {
            ItemVenda i = BuscarPorId(id);
            i.Quantidade++;
            Alterar(i);
        }

        public void DiminuirQuantidade(int id)
        {
            ItemVenda i = BuscarPorId(id);
            if (i.Quantidade > 1)
            {
                i.Quantidade--;
                Alterar(i);
            }
        }
    }
}
