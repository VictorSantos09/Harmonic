using Harmonic.API.Common;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.TipoConteudo.Contracts;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TipoConteudoController : ControllerBase, ISelfContainedController<TipoConteudoDTO, TipoConteudoEntity, int>
{
    private readonly ITipoConteudoAdicionarServices _adicionarConteudoService;
    private readonly ITipoConteudoDeletarServices _conteudoDeletarService;
    private readonly ITipoConteudoAtualizarServices _conteudoAtualizarService;
    private readonly ITipoConteudoGetServices _conteudoGetService;

    public TipoConteudoController(ITipoConteudoAdicionarServices adicionarConteudoService,
                              ITipoConteudoDeletarServices conteudoDeletarService,
                              ITipoConteudoAtualizarServices conteudoAtualizarService,
                              ITipoConteudoGetServices conteudoGetService)
    {
        _adicionarConteudoService = adicionarConteudoService;
        _conteudoDeletarService = conteudoDeletarService;
        _conteudoAtualizarService = conteudoAtualizarService;
        _conteudoGetService = conteudoGetService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(TipoConteudoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _adicionarConteudoService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoDeletarService.DeleteAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [GetAll]
    public async Task<ActionResult<IEnumerable<TipoConteudoEntity>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [GetById]
    public async Task<ActionResult<TipoConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(TipoConteudoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoAtualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
