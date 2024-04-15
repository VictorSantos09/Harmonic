using Harmonic.Regras.Services.Conteudo.Add;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConteudoController : ControllerBase
{
    private readonly IAdicionarConteudoService _adicionarConteudoService;

    public ConteudoController(IAdicionarConteudoService adicionarConteudoService)
    {
        _adicionarConteudoService = adicionarConteudoService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _adicionarConteudoService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    public Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [GetAll]
    public Task<ActionResult<IEnumerable<ConteudoDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [GetById]
    public Task<ActionResult<ConteudoDTO?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [Update]
    public Task<IActionResult> UpdateAsync(ConteudoDTO dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
