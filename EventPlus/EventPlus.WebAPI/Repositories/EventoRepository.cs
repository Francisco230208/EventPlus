using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository: IEventoRepository
{
    private readonly EventContext _context;


    public EventoRepository(EventContext context)
    {
        _context = context; 
    }

    /// <summary>
    /// Método para atualizar um evento, onde é possível alterar o nome, data, descrição, tipo e instituição do evento
    /// </summary>
    /// <param name="id">id do evento</param>
    /// <param name="evento"></param>
    public void Atualizar(Guid id,Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.DataEvento = evento.DataEvento;
            eventoBuscado.Nome = evento.Nome;
            eventoBuscado.IdTipoEvento =  evento.IdTipoEvento;
            eventoBuscado.IdInstituicao =  evento.IdInstituicao;
            _context .SaveChanges();
        }
    }

    /// <summary>
    /// Metodo de buscar um id especifico
    /// </summary>
    /// <param name="id">id do evento buscado</param>
    /// <returns>retorna o id do evento buscado</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id);
    }


    /// <summary>
    /// Metodo de cadastrar um novo evento
    /// </summary>
    /// <param name="evento"></param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Metodo de deletar um evento
    /// </summary>
    /// <param name="id">id a ser deletado</param>
    public void Deletar(Guid id)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Metodo para listar os eventos cadastrados
    /// </summary>
    /// <returns>retorna status code 204 e a lista de eventos</returns>
    public List<Evento> Listar()
    {

        return _context.Eventos
   .OrderBy(eventoBuscado => eventoBuscado.Nome)
   .ToList();
    }


    /// <summary>
    /// Método que busca eventos o qual um usário confirmou presença
    /// </summary>
    /// <param name="idUsuario">id so usuário a ser buscado</param>
    /// <returns>retorna lista de eventos</returns>
    public List<Evento> ListarPorId(Guid idUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == idUsuario && p.Situacao == true))
            .ToList();
    }


    /// <summary>
    /// Método que traz a lista de próximos eventos
    /// </summary>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ProximosEventos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
