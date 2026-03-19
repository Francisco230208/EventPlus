using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;
    
    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// EndPoint da APi pelo metodo de listar eventos filtrando pelo id do usuasrio
    /// </summary>
    /// <param name="IdUsuario">ID do usuario para filtragem</param>
    /// <returns>retorna status code 200 e uma lista de eventos</returns>
[HttpGet("Usuario/{IdUsuario}")]
public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
     }


    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar os próximos eventos
    /// </summary>
    /// <returns>retorna statusCode 200 e a lista dos próximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventos());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// Método para cadastrar um novo usuário
    /// </summary>
    /// <param name="eventoDTO"></param>
    /// <returns>retorna StatusCode 201 created</returns>
    [HttpPost("CadastrarProximos")]
    public IActionResult CadastrarProximosEventos(EventoDTO eventoDTO)
    {

        try
        {
            var evento = new Evento
            {
                Nome = eventoDTO.Nome!,
                Descricao = eventoDTO.Descricao!,
                DataEvento = eventoDTO.DataEvento ?? DateTime.Now,
                IdInstituicao = eventoDTO.IdInstituicao,
                IdTipoEvento = eventoDTO.IdTipoEvento

            };
           _eventoRepository.Cadastrar(evento);
            return Ok("Evento cadastrado com sucesso!");

        }
        catch (Exception erro)
        {
            return BadRequest(erro);
        }
    }

    [HttpGet("BuscarProximosEventosPorId/{id}")]
    public IActionResult BuscarPorID(Guid id)
    {
        try
        {
            var evento = _eventoRepository.BuscarPorId(id);
            if (evento == null)
            {
                return StatusCode(404);
            }
            return Ok(evento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventoDTO"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult AtualizarEventos(Guid id, EventoDTO eventoDTO)
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = eventoDTO.Nome!,
                Descricao = eventoDTO.Descricao!,
                DataEvento = eventoDTO.DataEvento ?? DateTime.Now,
                IdTipoEvento = eventoDTO.IdTipoEvento,
                IdInstituicao = eventoDTO.IdInstituicao
            };

            _eventoRepository.Atualizar(id, eventoAtualizado);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// Metodo deletar
    /// </summary>
    /// <param name="id">deleta um id específico</param>
    /// <returns>retorna o StatusCode 201</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}