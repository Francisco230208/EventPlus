using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories
{
    public class InstituicaoRepository : ITipoInstituicao
    {
        private readonly EventContext _context;

        //Injeção de dependência do contexto: Recebe o contexto pelo constutor
        public InstituicaoRepository(EventContext context)
        {
            _context = context;
        }
        /// <summary>
        /// metodo de atualizar uma instituicao
        /// </summary>
        /// <param name="id">busca o id de uma instituicao</param>
        /// <param name="instituicao">nom da insituicao</param>
        public void Atualizar(Guid id, Instituicao instituicao)
        {
            var TipoInstituicaoBuscada = _context.Instituicaos.Find(id);

            if (TipoInstituicaoBuscada != null)
            {
                TipoInstituicaoBuscada.Cnpj = String.IsNullOrWhiteSpace(instituicao.Cnpj)? TipoInstituicaoBuscada.Cnpj : instituicao.Cnpj;
                TipoInstituicaoBuscada.NomeFantasia = String.IsNullOrWhiteSpace(instituicao.NomeFantasia)? TipoInstituicaoBuscada.NomeFantasia : instituicao.NomeFantasia;
                TipoInstituicaoBuscada.Endereco = String.IsNullOrWhiteSpace(instituicao.Endereco) ? TipoInstituicaoBuscada.Endereco : instituicao.Endereco;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo de buscar uma instituicao especifica
        /// </summary>
        /// <param name="id">id da instituicao buscado</param>
        /// <returns>retorna o id buscado</returns>
        public Instituicao BuscarPorId(Guid id)
        {
            return _context.Instituicaos.Find(id)!;
        }


        /// <summary>
        /// Metodo de cadastrar uma nova instituicao
        /// </summary>
        /// <param name="instituicao">Nome d ainstituicao </param>
        public void Cadastrar(Instituicao instituicao)
        {

            _context .Instituicaos.Add(instituicao);
            _context.SaveChanges();
        }

        /// <summary>
        /// Metodo de deletar uma instituicao
        /// </summary>
        /// <param name="id">id deletado</param>
        public void Deletar(Guid id)
        {
            var InstituicaoBuscada = _context.Instituicaos.Find(id);
            if (InstituicaoBuscada != null)
            {
                _context.Instituicaos.Remove(InstituicaoBuscada);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// metodo de listar todas as instituicoes cadastradas
        /// </summary>
        /// <returns></returns>
        public List<Instituicao> Listar()
        {
            return _context.Instituicaos
       .OrderBy(instituicao => instituicao.NomeFantasia)
       .ToList();
        }
    }
}
