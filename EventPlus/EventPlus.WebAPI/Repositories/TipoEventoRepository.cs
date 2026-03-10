using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoEventoRepository : ITipoEventoRepository
{
    private readonly EventContext _context;

    //Injeção de dependência do contexto: Recebe o contexto pelo constutor
    public TipoEventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um tipo de evento usando o rastreamento automático
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="tipoEvento">Novos dados do tipo evento</param>
    

    public void Atualizar(Guid id, TipoEvento tipoEvento)
    {
        var TipoEventoBuscado = _context.TipoEventos.Find(id);

        if (TipoEventoBuscado != null)
        {
            TipoEventoBuscado.Titulo = tipoEvento.Titulo;
            //O SaveChanges decta a mudança no banco de dados  na propriedade "Titulo" automaticamente
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de evento por id
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>
    /// <returns>Objeto do tipo evento buscado com as informações do tipo de evento buscado</returns>
    public TipoEvento BuscarPorId(Guid id)
    {
        return _context.TipoEventos.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>

    public void Cadastrar(TipoEvento tipoEvento)
    {
        _context.TipoEventos.Add(tipoEvento);
        _context.SaveChanges();
    }


    /// <summary>
    /// Deleta um tipo de evento
    /// </summary>
    /// <param name="id">id do tipo delete</param>
    public void Deletar(Guid id)
    {
       var TipoEventoBuscado = _context.TipoEventos.Find(id);
        if (TipoEventoBuscado != null)
        {
            _context.TipoEventos.Remove(TipoEventoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Lista todos TipoEventos no banco de dados
    /// </summary>
    /// <returns>Lista de TipoEvento encontrados</returns>
    public List<TipoEvento> Listar()
    {
        return _context.TipoEventos
            .OrderBy(tipoEvento => tipoEvento.Titulo)
            .ToList();

    }
}
