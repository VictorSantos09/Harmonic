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
public class PaisController : ControllerBase, IAddController<PaisDTO>
{
    private readonly IPaisAdicionarService _adicionarPaisService;

    public PaisController(IPaisAdicionarService adicionarPaisService)
    {
        _adicionarPaisService = adicionarPaisService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(PaisDTO dto, CancellationToken cancellationToken)
    {
        var result = await _adicionarPaisService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
