using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;

    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// EndPoint da API que cadastra e modera um comentario
    /// </summary>
    /// <param name="comentarioEvento">comentario a ser moderado</param>
    /// <returns>StatusCode 201 e o comentario a ser criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio.");
            }

            //criar objeto de análise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            // chamar a API do Azure content safety
            Response<AnalyzeTextResult> response = await
            _contentSafetyClient.AnalyzeTextAsync(request);

            //verificar se o texto tem alguma severidade > 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentarioEvento => comentarioEvento.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentarioEvento = DateTime.Now,
                //Define se o comentario vai ser exibido
                Exibe = !temConteudoImproprio,
            };

            //Cadastrar o comentario

            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{idUsuario}/{idEvento}")]
    public IActionResult ComentarioUsuarioEvento(Guid idUsuario, Guid idEvento)
    {
        try
        {
        var comentario = _comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento);

            if (comentario == null)
            {
                return NotFound("Comentario não encontrado");

            }
            return Ok(comentario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);

        }
    }


    [HttpDelete]
    public IActionResult Deletar(Guid idComentario)
    {
        try
        {
            _comentarioEventoRepository.Deletar(idComentario);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpGet("ComentariosEventoTodos/{idEvento}")]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("ExibiTrue{IdEvento}")]
    public IActionResult ListarSomenteExibe(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

}
