using Exo.WebApi.Contexts;

namespace Exo.WebApi.Repository
{
    public class UsuarioRepository
    {
        private readonly ExoContext _context;
        public UsuarioRepository(ExoContext Context)
        {
            _context = Context;
        }
        public Usuario Login(string Email, string Senha)
        {
            return _context.usuarios.FirstOrDefault(u => u.Email == Email && u.Senha == Senha);
        }
        public List<Usuario> Listar()
        {
            return _context.usuarios.ToList();
        }
        public void Cadastrar(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
            _context.SaveChanges();
        }
        public Usuario BuscarPorId(int id)
        {
            return _context.usuarios.Find(id);
        }
        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioBuscado = _context.usuarios.Find(id);
            if(usuarioBuscado != null)
            {
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
            }
            _context.usuarios.Update(usuarioBuscado);
            _context.SaveChanges();
        }
        public void Deletar(int id)
        {
            Usuario usuarioBuscado = _context.usuarios.Find(id);
            _context.usuarios.Remove(usuarioBuscado);
            _context.SaveChanges();
        }
    }
}