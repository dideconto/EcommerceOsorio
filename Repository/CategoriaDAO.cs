using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    class CategoriaDAO : IRepository<Produto>
    {
        public Produto BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Cadastrar(Produto objeto)
        {
            throw new NotImplementedException();
        }

        public List<Produto> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
