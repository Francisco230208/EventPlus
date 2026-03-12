using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private  ITipoInstituicao _instituicaoRepository;//Atributo do tipo de evento repository para acessar os métodos do repositório


    public InstituicaoController(ITipoInstituicao InstituicaoRepository)
    {
        _instituicaoRepository = InstituicaoRepository;
    }


    /// <summary>
    /// EndPoint da API que afz chamada para o método de listar os tipos de instituição
    /// </summary>
    /// <returns>Retorna o StatusCode 200</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um tipo de instituição específico
    /// </summary>
    /// <param name="id">Procura um tipo de id de instiuicao</param>
    /// <returns>retorna status code 200</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de cadastrar um tipo de instituicao
    /// </summary>
    /// <param name="instituicao"></param>
    /// <returns>retorna StatusCode 201 created</returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
           var novaInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnjp!,
                Endereco = instituicao.Endereco!
           };

            _instituicaoRepository.Cadastrar(novaInstituicao);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPoint da APi que faz o metodo atualizar um tipo de instituição específico
    /// <param name="id">id od tipo de instituicao a ser atualizado</param>
    /// <param name="instituicao"></param>
    /// <returns>retorna StatusCode 204, metodo atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia,
                Cnpj = instituicao.Cnjp,
                Endereco = instituicao.Endereco
            };

            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    /// <summary>
    /// EndPOint da API que faz a chamada para o método de deletar um tipo de instituição específico
    /// </summary>
    /// <param name="id">id de instituicao a ser excluido</param>
    /// <returns>retorna satus 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}