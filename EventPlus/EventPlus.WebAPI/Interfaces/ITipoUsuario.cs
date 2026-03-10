using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface ITipoUsuario
{
    List<TipoUsuario> Listar();
    void Cadastrar (TipoUsuario tipoUsuario);
     void Atualizar(Guid id, TipoUsuario tipoUsuario);
     void Deletar(Guid id);
     TipoUsuario BuscarPorId(Guid id);
}
