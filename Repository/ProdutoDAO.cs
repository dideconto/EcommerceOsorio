using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ProdutoDAO : IRepository<Produto>
    {
        private readonly Context _context;
        public ProdutoDAO(Context context)
        {
            _context = context;
        }
        public bool Cadastrar(Produto p)
        {
            if (BuscarProdutoPorNome(p) == null)
            {
                _context.Produtos.Add(p);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Produto BuscarProdutoPorNome(Produto p)
        {
            return _context.Produtos.FirstOrDefault
                (x => x.Nome.Equals(p.Nome));
        }
        public List<Produto> ListarTodos()
        {
            return _context.Produtos.ToList();
        }
        public Produto BuscarPorId(int id)
        {
            return _context.Produtos.Find(id);
        }
        public void RemoverProduto(int id)
        {
            _context.Produtos.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }
        public void Alterar(Produto p)
        {
            _context.Produtos.Update(p);
            _context.SaveChanges();
        }

        public List<Produto> ListarPorCategoria(int? id)
        {
            return _context.Produtos.Include(x => x.Categoria).Where(x => x.Categoria.CategoriaId == id).ToList();
        }
    }
}
