using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Pais.Add;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.AspNetCore.Controllers.Contracts;
using QuickKit.ResultTypes.Converters;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Harmonic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaisController : ControllerBase, IAddController<PaisDTO>
{
    private readonly IAdicionarPaisService _adicionarPaisService;

    public PaisController(IAdicionarPaisService adicionarPaisService)
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
