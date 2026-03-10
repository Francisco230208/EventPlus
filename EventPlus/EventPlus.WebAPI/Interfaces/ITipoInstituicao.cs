using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface ITipoInstituicao
{
    List<Instituicao> Listar();
        void Cadastrar (Instituicao instituicao);
        void Atualizar(Guid id, Instituicao instituicao);
        void Deletar(Guid id);
        Instituicao BuscarPorId(Guid id);
}
