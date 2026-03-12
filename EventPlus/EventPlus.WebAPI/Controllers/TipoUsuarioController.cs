using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuario _tipoUsuarioRepository;//Atributo do tipo de evento repository para acessar os métodos do repositório

    public TipoUsuarioController(ITipoUsuario tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }


    /// <summary>
    /// EndPoint da API que afz chamada para o método de listar os tipos de cadastro de usuário
    /// </summary>
    /// <returns>status code 200 e a lista de tipo de cadastro</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }

    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um tipo de cadastro específico
    /// </summary>
    /// <param name="id">Id um tipo de cadastro buscado</param>
    /// <returns>status code 200 tipo de cadastro buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPoint da API que faz a chamada para o método de cadastrar um tipo de usuario
    /// </summary>
    /// <param name="tipoUsuario">Tipo de usuario a ser cadastrado</param>
    /// <returns>status code 201 created, e o tipo de usuario cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {

            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
                
            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201, novoTipoUsuario);//201 Createda
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPoint da APi que faz a chamada para o metdo de atualizar um tipo de usuario 
    /// </summary>
    /// <param name="id">Id do tipo de evento a ser atualizado e tipo de usuario com os dados atualizados</param>
    /// <param name="tipoUsuario"></param>
    /// <returns>StatusCode 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
     public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
     {
         try
         {
             var tipoUsuarioAtualizado = new TipoUsuario
             {
                 Titulo = tipoUsuario.Titulo!
             };
             _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);
             return NoContent();//204 NoContent
         }
         catch (Exception erro)
         {
             return BadRequest(erro.Message);
         }
    }

    /// <summary>
    /// EndPoint do método deletar um tipo de usuario, que faz a chamada para o método de deletar um tipo de usuario
    /// </summary>
    /// <param name="id">id do tipo de evento  a ser excluido </param>
    /// <returns>StatusCode</returns>

    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return NoContent();//204 NoContent
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
