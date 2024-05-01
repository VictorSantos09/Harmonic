using Harmonic.API.Common;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Pais.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.AspNetCore.Controllers.Contracts;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PaisController : ControllerBase, IAddController<PaisDTO>, ISelfContainedController<PaisDTO, PaisEntity, int>
{
    private readonly IPaisAdicionarService _adicionarPaisService;
    private readonly IPaisGetService _paisGetService;
    private readonly IPaisAtualizarService _paisAtualizarService;
    private readonly IPaisDeletarService _paisDeletarService;

    public PaisController(IPaisAdicionarService adicionarPaisService,
                          IPaisGetService paisGetService,
                          IPaisAtualizarService paisAtualizarService,
                          IPaisDeletarService paisDeletarService)
    {
        _adicionarPaisService = adicionarPaisService;
        _paisGetService = paisGetService;
        _paisAtualizarService = paisAtualizarService;
        _paisDeletarService = paisDeletarService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(PaisDTO dto, CancellationToken cancellationToken)
    {
        var result = await _adicionarPaisService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _paisDeletarService.DeleteAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [GetById]
    public async Task<ActionResult<PaisEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _paisGetService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(PaisDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _paisAtualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [GetAll]
    public async Task<ActionResult<IEnumerable<PaisEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _paisGetService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
