using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId (Guid  id);
    List<Usuario> Listar();
    Usuario BuscarPorEmailESenha(string Email, string Senha);
}
