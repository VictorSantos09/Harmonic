using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class ConteudoPlataformaController
{
    private readonly IConteudoPlataformaAdicionarService _adicionarService;
    private readonly IConteudoPlataformaDeletarService _deletarService;
    private readonly IConteudoPlataformaAtualizarService _atualizarService;
    private readonly IConteudoPlataformaGetService _getService;

    public ConteudoPlataformaController(IConteudoPlataformaAdicionarService adicionarService,
                              IConteudoPlataformaDeletarService deletarService,
                              IConteudoPlataformaAtualizarService atualizarService,
                              IConteudoPlataformaGetService getService)
    {
        _adicionarService = adicionarService;
        _deletarService = deletarService;
        _atualizarService = atualizarService;
        _getService = getService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(ConteudoPlataformaDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _adicionarService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _deletarService.DeleteAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [GetAll]
    public async Task<ActionResult<IEnumerable<ConteudoPlataformaEntity>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _getService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [GetById]
    public async Task<ActionResult<ConteudoPlataformaEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _getService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(ConteudoPlataformaDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _atualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
