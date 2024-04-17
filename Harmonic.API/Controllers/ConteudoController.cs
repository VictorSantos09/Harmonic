using Harmonic.API.Common;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ConteudoController : ControllerBase, ISelfContainedController<ConteudoDTO, ConteudoEntity, int>
{
    private readonly IConteudoAdicionarService _adicionarConteudoService;
    private readonly IConteudoDeletarService _conteudoDeletarService;
    private readonly IConteudoAtualizarService _conteudoAtualizarService;
    private readonly IConteudoGetService _conteudoGetService;

    public ConteudoController(IConteudoAdicionarService adicionarConteudoService,
                              IConteudoDeletarService conteudoDeletarService,
                              IConteudoAtualizarService conteudoAtualizarService,
                              IConteudoGetService conteudoGetService)
    {
        _adicionarConteudoService = adicionarConteudoService;
        _conteudoDeletarService = conteudoDeletarService;
        _conteudoAtualizarService = conteudoAtualizarService;
        _conteudoGetService = conteudoGetService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken = default)
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
    public async Task<ActionResult<IEnumerable<ConteudoEntity>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [GetById]
    public async Task<ActionResult<ConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoGetService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(ConteudoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoAtualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
