﻿using EcommerceOsorio.Models;
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
        public List<Produto> ListarProdutos()
        {
            return _context.Produtos.ToList();
        }

        public Produto BuscarProdutoPorId(int id)
        {
            return _context.Produtos.Find(id);
        }
        public void RemoverProduto(int id)
        {
            _context.Produtos.Remove(BuscarProdutoPorId(id));
            _context.SaveChanges();
        }

        public void Alterar(Produto p)
        {
            _context.Produtos.Update(p);
            _context.SaveChanges();
        }
    }
}
