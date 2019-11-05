using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UsuarioDAO : IRepository<Usuario>
    {
        private readonly Context _context;
        public UsuarioDAO(Context context)
        {
            _context = context;
        }
        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }
        public bool Cadastrar(Usuario u)
        {
            if(BuscarPorEmail(u) == null)
            {
                _context.Usuarios.Add(u);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Usuario BuscarPorEmail(Usuario u)
        {
            return _context.Usuarios.FirstOrDefault
                (x => x.Email.Equals(u.Email));
        }
        public List<Usuario> ListarTodos()
        {
            return _context.Usuarios.ToList();
        }
    }
}
