using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresençaController : ControllerBase
{
    private readonly IPresençaRepository _presençaRepository;//Injeção de dependência do repositório: Recebe o repositório pelo construtor
    public PresençaController(IPresençaRepository presençaRepository)
    {
        _presençaRepository = presençaRepository;
    }


    /// <summary>
    /// EndPoint da API que retorna uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>Status Code 200 e retorna a presença buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presençaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que retorna uma lista de presenças filtrando pelo id do usuário
    /// </summary>
    /// <param name="idUsuario">id do usuario para foltragem</param>
    /// <returns>Uma lista de presenças filtradas</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult ListarMinhas(Guid idUsuario)
    {
        try
        {
            return Ok(_presençaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presençaRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPoint da APi que lista todas as presenças
    /// </summary>
    /// <returns>retorna as presenças listadas</returns>
    /// <summary>
    /// EndPoint da API que Inscreve/Cadastra uma nova presença
    /// </summary>
    /// <param name="presenca">Nome da presença cadastrada</param>
    /// <returns>Status Code 201 e retorna a presença cadastrada</returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdUsuario = presenca.IdUsuario,
                IdEvento = presenca.IdEvento
            };
            _presençaRepository.Inscrever(novaPresenca);
            return Ok(novaPresenca);
            
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que atualiza uma presença existente
    /// </summary>
    /// <param name="id">Busca o id da presença a ser atualizada</param>
    /// <param name="presenca"></param>
    /// <returns>retorna Status Code 204 e a lista de presenças atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presencaDto)
    {
        try
        {
        
            var presencaAtualizada = new Presenca
            {
                Situacao = presencaDto.Situacao,
                IdUsuario = presencaDto.IdUsuario,
                IdEvento = presencaDto.IdEvento
            };
            _presençaRepository.Atualizar(id, presencaAtualizada);
            return StatusCode(204, presencaAtualizada);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que deleta uma presença existente
    /// </summary>
    /// <param name="id">id a ser deletado</param>
    /// <returns>retorna Status Code 204 e deleta o id</returns>
    [HttpDelete]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presençaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
