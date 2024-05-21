using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Services.ConteudoReacao;
using Harmonic.Regras.Services.ConteudoReacao.DTO;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;

namespace Harmonic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConteudoReacaoController : ControllerBase
{
    private readonly IConteudoReacaoService _conteudoReacaoService;

    public ConteudoReacaoController(IConteudoReacaoService conteudoReacaoService)
    {
        _conteudoReacaoService = conteudoReacaoService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoService.AddAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoService.UpdateAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoService.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);
        return result.IsSuccess ? Ok() : NotFound(result);
    }
}
