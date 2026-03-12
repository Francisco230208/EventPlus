using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;//Atributo do tipo de evento repository para acessar os métodos do repositório

    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }





    /// <summary>
    /// EndPoint da API que afz chamada para o método de listar os tipos de evento
    /// </summary>
    /// <returns>status code 200 e a lista de tipo de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
        
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um tipo de evento específico
    /// </summary>
    /// <param name="id">Id um tipo de evento buscado</param>
    /// <returns>status code 200 tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de cadastrar um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>status code 201 created, e e o tipo de evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastro(TipoEventoDTO tipoEvento)
    {
        try
        { 
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!

            };


            _tipoEventoRepository.Cadastrar(novoTipoEvento);
            return StatusCode(201, novoTipoEvento);//201 Createda
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// EndPoint da APi que faz a chamada para o metdo de atualizar um tipo de evento 
    /// </summary>
    /// <param name="id">Id do tipo de evento a ser atualizado e tipo de evento com os dados atualizados</param>
    /// <param name="tipoEvento"></param>
    /// <returns>StatusCode 204 e o tipo de evento atualizado</returns>

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento)
    {
        try
        {

            var tipoEventoAtualizado = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };

            _tipoEventoRepository.Atualizar(id, tipoEventoAtualizado);
            return StatusCode (204, tipoEventoAtualizado);//204 NoContent
        }
        catch(Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de deletar um tipo de evento
    /// </summary>
    /// <param name="id">id do tipo do evento as er excluido</param>
    /// <returns>StatusCode</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return NoContent();//tipo de jeito de colocar o no content (204)
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}