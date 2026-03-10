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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="instituicao"></param>
        public void Atualizar(Guid id, Instituicao instituicao)
        {
            var TipoInstituicaoBuscada = _context.Instituicaos.Find(id);

            if (TipoInstituicaoBuscada != null)
            {
                TipoInstituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
                TipoInstituicaoBuscada.Cnpj = instituicao.Cnpj;
                TipoInstituicaoBuscada.Endereco = instituicao.Endereco;
                _context.SaveChanges();
            }
        }
        public Instituicao BuscarPorId(Guid id)
        {
            return _context.Instituicaos.Find(id)!;
        }

        public void Cadastrar(Instituicao instituicao)
        {

            _context .Instituicaos.Add(instituicao);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var InstituicaoBuscada = _context.Instituicaos.Find(id);
            if (InstituicaoBuscada != null)
            {
                _context.Instituicaos.Remove(InstituicaoBuscada);
                _context.SaveChanges();
            }
        }

        public List<Instituicao> Listar()
        {
            return _context.Instituicaos
       .OrderBy(instituicao => instituicao.NomeFantasia)
       .ToList();
        }
    }
}
