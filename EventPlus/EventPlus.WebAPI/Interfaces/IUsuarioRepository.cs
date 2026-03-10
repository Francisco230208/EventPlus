using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId (Guid  id);
    Usuario BuscarPorEmailESenha(string Email, string Senha);
}
