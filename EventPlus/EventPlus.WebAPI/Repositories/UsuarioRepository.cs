using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository: IUsuarioRepository
{
    private readonly EventContext _context;

    //Injeção de dependência do contexto: Recebe o contexto pelo constutor
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Busca  o usuario pelo email e valida o hash da senha
    /// </summary>
    /// <param name="Email">Usuario a ser buscado</param>
    /// <param name="Senha">Senha para validar o usuario</param>
    /// <returns>retorna o usuario buscado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        //Primeiro, buscamos o usuário pelo e-mail
        var usuarioBuscado = _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.Email == Email);

        //Verificamos se o usuario foi encontrado
        if (usuarioBuscado != null)
        {
            //comparamos o hash da seha digitada ocm o que esta no banco
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
            
        }
        return null!;
    }


    /// <summary>
    ///Busca um usuário por ID, incluindo os dados do seu tipo de usuário 
    /// </summary>
    /// <param name="id">id do usuário a ser buscado</param>
    /// <returns>Usuário Buscado e seu tipo de usuario</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }


    /// <summary>
    /// cadastra um novo usuário.A s    enha é criptografada e o ID gerado pelo banco
    /// </summary>
    /// <param name="usuario">Usuário a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
    public List<Usuario> Listar()
    {
        return _context.Usuarios.ToList();
    }
}
