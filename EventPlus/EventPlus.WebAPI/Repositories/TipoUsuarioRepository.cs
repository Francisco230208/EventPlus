using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        private readonly EventContext _context;

        //Injeção de dependência do contexto: Recebe o contexto pelo constutor
        public TipoUsuarioRepository(EventContext context)
        {
            _context = context;
        }
       
    /// <summary>
    /// Atualiza um tipo de evento usando o rastreamento automático
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="tipoUsuario">Novos dados do tipo evento</param>
        public void Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            var TipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

            if ( TipoUsuarioBuscado!= null)
            {
                TipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
                //O SaveChanges decta a mudança no banco de dados  na propriedade "Titulo" automaticamente
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Busca um tipo de evento por id
        /// </summary>
        /// <param name="id">id do tipo evento a ser buscado</param>
        /// <returns>Objeto do tipo evento buscado com as informações do tipo de evento buscado</returns>
        public TipoUsuario BuscarPorId(Guid id)
        {
            return _context.TipoUsuarios.Find(id)!;
        }

        public void Cadastrar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
        }


        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="id">id do tipo delete</param>
        public void Deletar(Guid id)
        {
            var TipoUsuario = _context.TipoUsuarios.Find(id);
            if (TipoUsuario!!= null)
            {
                _context.TipoUsuarios.Remove(TipoUsuario);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Lista todos TipoUsuario no banco de dados
        /// </summary>
        /// <returns>Lista de TipoUsuario encontrados</returns>
        public List<TipoUsuario> Listar()
        {
           return _context.TipoUsuarios.OrderBy(TipoUsuario => TipoUsuario).ToList();
        }
    }
}
