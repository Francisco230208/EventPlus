using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository: IComentarioEventoRepository
{
    private readonly EventContext _context;
   
    public ComentarioEventoRepository(EventContext context)
    {
            _context = context;
    }

    /// <summary>
    /// Metodo para buscar um comentario de um evento por id do usuario e id do evento
    /// </summary>
    /// <param name="idUsuario">id do usuario a ser buscado</param>
    /// <param name="IdEvento">id do evento a ser buscado</param>
    /// <returns>retorna status code 204 e a lista dos usuarios buscados</returns>
    public ComentarioEvento BuscarPorIdUsuario(Guid idUsuario, Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdEvento == IdEvento)!;

    }


    /// <summary>
    /// Metodo para cadastrar um novo tipo de evento
    /// </summary>
    /// <param name="comentarioEvento">cadastro de um comentario</param>
    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }


    /// <summary>
    /// Metodo para deletar um comentario de um evento por id do usuario e id do evento
    /// </summary>
    /// <param name="id">id do comentario a ser deletado</param>
    public void Deletar(Guid id)
    {
       var comentarioEventoBuscado = _context.ComentarioEventos.Find(id);
        if (comentarioEventoBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioEventoBuscado);
            _context.SaveChanges();
        }
    }


    /// <summary>
    /// Metodo para listar os comentarios de um evento por id do evento
    /// </summary>
    /// <param name="IdEvento">id dos comentarios do evento</param>
    /// <returns>retorna status code 204 e a lista de comentarios</returns>
    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
       return _context.ComentarioEventos.OrderBy(comentarioEventoBuscado => comentarioEventoBuscado.Descricao).ToList();
    }

    /// <summary>
    /// metodo que lista somente um id especifico
    /// </summary>
    /// <param name="IdEvento">lista somente um id especifico do evento</param>
    /// <returns>retorna status code 204 e a lista especifica de id</returns>
    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
       .Where(comentarioEventoBuscado => comentarioEventoBuscado.Exibe == true)
       .OrderBy(comentarioEventoBuscado => comentarioEventoBuscado.Descricao)
       .ToList();
    }
} 
