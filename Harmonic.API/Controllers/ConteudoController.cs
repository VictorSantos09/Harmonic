using Harmonic.API.Common;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes;
using QuickKit.ResultTypes.Converters;
using System.Linq;
using System.Net;

namespace Harmonic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConteudoController : ControllerBase, ISelfContainedController<ConteudoDTO, ConteudoEntity, int>
{
    private readonly IConteudoAdicionarService _adicionarConteudoService;
    private readonly IConteudoDeletarService _conteudoDeletarService;
    private readonly IConteudoAtualizarService _conteudoAtualizarService;
    private readonly IConteudoGetService _conteudoGetService;
    private readonly IConteudoPlataformaAdicionarService _conteudoPlataformaAdicionarService;
    private readonly IPlataformaGetService _plataformaGetService;

    public ConteudoController(IConteudoAdicionarService adicionarConteudoService,
                              IConteudoDeletarService conteudoDeletarService,
                              IConteudoAtualizarService conteudoAtualizarService,
                              IConteudoGetService conteudoGetService,
                              IConteudoPlataformaAdicionarService conteudoPlataformaAdicionar,
                              IPlataformaGetService plataformaGetService)
    {
        _adicionarConteudoService = adicionarConteudoService;
        _conteudoDeletarService = conteudoDeletarService;
        _conteudoAtualizarService = conteudoAtualizarService;
        _conteudoGetService = conteudoGetService;
        _conteudoPlataformaAdicionarService = conteudoPlataformaAdicionar;
        _plataformaGetService = plataformaGetService;
    }

    [Add]
    [Authorize]
    public async Task<IActionResult> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken = default)
    {


        

        var result = await _adicionarConteudoService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoDeletarService.DeleteAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [HttpDelete("range")]
    [Authorize]
    public async Task<IActionResult> DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        await _conteudoDeletarService.DeleteRangeAsync(ids, cancellationToken);
        return Ok();
    }

    [GetAll]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ConteudoEntity>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [GetById]
    [Authorize]
    public async Task<ActionResult<ConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [HttpGet("top-radios")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ConteudoTopEntity>>> GetTopRadiosAsync(CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetTopRadiosAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [HttpGet("top-podcasts")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ConteudoTopEntity>>> GetTopPodcastsAsync(CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetTopPodcastsAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [HttpGet("detalhes")]
    [AllowAnonymous]
    public async Task<ActionResult<ConteudoDetalhesDto>> GetDetalhesAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetDetalhesAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [HttpGet("conteudo-plataformas-url")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<string>>> GetConteudoPlataformasURL(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetConteudoPlataformasURL(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(ConteudoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoAtualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
