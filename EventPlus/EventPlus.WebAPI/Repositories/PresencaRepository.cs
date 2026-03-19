using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository: IPresençaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }


    /// <summary>
    /// Método para atualizar uma presença existente
    /// </summary>
    /// <param name="Id">Busca o id da presença</param>
    public void Atualizar(Guid Id, Presenca presenca)
    {
        var presencaBuscada = _eventContext.Presencas.Find(Id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;
            _eventContext.SaveChanges();
        }
    }

    public void Atualizar(Guid Id)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="Id">id da presença a ser buscada</param>
    /// <returns>Retorna a presença buscada</returns>
    public Presenca BuscarPorId(Guid Id)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == Id)!;
    }

    /// <summary>
    /// Método para deletar uma presença
    /// </summary>
    /// <param name="Id">Id do tipo delete </param>
    public void Deletar(Guid Id)
    {
       var presencaBuscada = _eventContext.Presencas.Find(Id);
        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
            _eventContext.SaveChanges();
        }
    }


    /// <summary>
    /// Método para Inscrever um usuário em um evento, criando uma nova presença
    /// </summary>
    /// <param name="Inscricao">Tipo de presença a ser cadastrado</param>
    public void Inscrever(Presenca Inscricao)
    {
        var novaInscricao = new Presenca
        {
            Situacao = Inscricao.Situacao,
            IdUsuario = Inscricao.IdUsuario,
            IdEvento = Inscricao.IdEvento
        };
        _eventContext.Presencas.Add(novaInscricao);
        _eventContext.SaveChanges();
    }

    /// <summary>
    /// Método para listar todas as presenças cadastradas no sistema
    /// </summary>
    /// <returns>Retorna uma lista com as presenças</returns>
    public List<Presenca> Listar()
    {
        return _eventContext.Presencas.OrderBy(Presenca => Presenca.Situacao).ToList();
    }

    /// <summary>
    /// Lista as presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">id do usuário para  a filtragem</param>
    /// <returns>retorna a lista de presenças de um usuário específico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
