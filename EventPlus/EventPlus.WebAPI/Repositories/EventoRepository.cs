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

    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id);
    }


    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

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
