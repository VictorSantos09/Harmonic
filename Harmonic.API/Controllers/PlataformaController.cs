using Harmonic.API.Common;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;


[ApiController]
[Route("[controller]")]
public class PlataformaController : ControllerBase, ISelfContainedController<PlataformaDTO, PlataformaEntity, int>
{
    private readonly IPlataformaAdicionarService _adicionarPlataformaService;
    private readonly IPlataformaDeletarService _plataformaDeletarService;
    private readonly IPlataformaAtualizarService _plataformaAtualizarService;
    private readonly IPlataformaGetService _plataformaGetService;

    public PlataformaController(IPlataformaAdicionarService adicionarPlataformaService,
                              IPlataformaDeletarService plataformaDeletarService,
                              IPlataformaAtualizarService plataforamaAtualizarService,
                              IPlataformaGetService plataformaGetService)
    {
        _adicionarPlataformaService = adicionarPlataformaService;
        _plataformaDeletarService = plataformaDeletarService;
        _plataformaAtualizarService = plataforamaAtualizarService;
        _plataformaGetService = plataformaGetService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(PlataformaDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _adicionarPlataformaService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _plataformaDeletarService.DeleteAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [GetAll]
    public async Task<ActionResult<IEnumerable<PlataformaEntity>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _plataformaGetService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [GetById]
    public async Task<ActionResult<PlataformaEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _plataformaGetService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(PlataformaDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _plataformaAtualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
