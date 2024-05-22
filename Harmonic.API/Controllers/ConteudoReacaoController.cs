using Harmonic.API.Context;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Services.ConteudoReacao;
using Harmonic.Regras.Services.ConteudoReacao.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ConteudoReacaoController : ControllerBase
{
    private readonly IConteudoReacaoService _conteudoReacaoService;
    private readonly UserManager<HarmonicIdentityUser> _userManager;

    public ConteudoReacaoController(IConteudoReacaoService conteudoReacaoService,
                                    UserManager<HarmonicIdentityUser> userManager)
    {
        _conteudoReacaoService = conteudoReacaoService;
        _userManager = userManager;
    }

    [Add("like")]
    public async Task<IActionResult> LikeAsync(int idConteudo, CancellationToken cancellationToken = default)
    {
        var idUsuario = _userManager.GetUserId(User);

        if (idUsuario is null) return Unauthorized();

        ConteudoReacaoDTO dto = new(idConteudo, idUsuario, true);

        var result = await _conteudoReacaoService.AddAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [Add("deslike")]
    public async Task<IActionResult> DeslikeAsync(int idConteudo, CancellationToken cancellationToken = default)
    {
        var idUsuario = _userManager.GetUserId(User);

        if (idUsuario is null) return Unauthorized();

        ConteudoReacaoDTO dto = new(idConteudo, idUsuario, false);

        var result = await _conteudoReacaoService.AddAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var idUsuario = _userManager.GetUserId(User);

        if (idUsuario is null) return Unauthorized();

        var result = await _conteudoReacaoService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(int idConteudo, bool curtiu, CancellationToken cancellationToken = default)
    {
        var idUsuario = _userManager.GetUserId(User);

        if (idUsuario is null) return Unauthorized();

        ConteudoReacaoDTO dto = new(idConteudo, idUsuario, curtiu);
        var result = await _conteudoReacaoService.UpdateAsync(dto, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarioConteudoReacaoAsync(int idConteudo, CancellationToken cancellationToken = default)
    {
        var idUsuario = _userManager.GetUserId(User);

        if (idUsuario is null) return Unauthorized();

        var result = await _conteudoReacaoService.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);
        return result.IsSuccess ? Ok() : NotFound(result);
    }

}
