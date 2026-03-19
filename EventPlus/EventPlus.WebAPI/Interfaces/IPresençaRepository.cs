using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresençaRepository
{
    void Inscrever(Presenca Inscricao);
    void Deletar(Guid Id);
    List<Presenca> Listar();
    Presenca BuscarPorId (Guid Id);
    void Atualizar(Guid Id);
    List<Presenca> ListarMinhas(Guid IdUsuario);
    void Atualizar(Guid id, Presenca presenca);
}
