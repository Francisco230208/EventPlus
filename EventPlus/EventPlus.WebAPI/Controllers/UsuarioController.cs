using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }


 
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um usuário específico, buscando pelo id do usuário
    /// </summary>
    /// <param name="id">id do usuário a ser buscado</param>
    /// <returns>retorna StatusCode 200 e o usuário buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            {
                return Ok(_usuarioRepository.BuscarPorId(id));
            }
            
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }



    /// <summary>
    /// EndPoint da API que faz a chamada para o método cadastrar um usuário
    /// </summary>
    /// <param name="usuario">Usuário a ser cadastrado</param>
    /// <returns>Retorna StatusCode 201 e o Usuario cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuarioDto)
    {
        var  usuario = new Usuario
        {
            
            Nome = usuarioDto.Nome!,
            Email = usuarioDto.Email!,
            Senha = usuarioDto.Senha!,
            IdTipoUsuario = usuarioDto.IdTipoUSuario
        };

        try
        {
            _usuarioRepository.Cadastrar(usuario);
            return StatusCode(201, usuario);       
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// EndPOint da API que lista todos os usuarios
    /// </summary>
    /// <returns>retorna status code 204 e lista os usuarios</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_usuarioRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
