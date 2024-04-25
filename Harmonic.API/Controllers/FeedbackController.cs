using Harmonic.API.Common;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Feedback.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickKit.AspNetCore.Attributes;
using QuickKit.ResultTypes.Converters;
using System.Net;

namespace Harmonic.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ControllerBase, ISelfContainedController<FeedbackDTO, FeedbackEntity, int>
{
    private readonly IFeedbackAdicionarService _feedbackAdicionarService;
    private readonly IFeedbackDeletarService _feedbackDeletarService;
    private readonly IFeedbackAtualizarService _feedbackAtualizarService;
    private readonly IFeedbackGetService _feedbackGetService;

    public FeedbackController(IFeedbackAdicionarService feedbackAdicionarService,
                              IFeedbackDeletarService feedbackDeletarService,
                              IFeedbackAtualizarService feedbackAtualizarService,
                              IFeedbackGetService feedbackGetService)
    {
        _feedbackAdicionarService = feedbackAdicionarService;
        _feedbackDeletarService = feedbackDeletarService;
        _feedbackAtualizarService = feedbackAtualizarService;
        _feedbackGetService = feedbackGetService;
    }

    [Add]
    public async Task<IActionResult> AddAsync(FeedbackDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _feedbackAdicionarService.AddAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [Delete]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _feedbackDeletarService.DeleteAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }

    [GetAll]
    public async Task<ActionResult<IEnumerable<FeedbackEntity>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _feedbackGetService.GetAllAsync(cancellationToken);
        return result.Convert(HttpStatusCode.NoContent);
    }

    [GetById]
    public async Task<ActionResult<FeedbackEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _feedbackGetService.GetByIdAsync(id, cancellationToken);
        return result.Convert(HttpStatusCode.NotFound);
    }

    [Update]
    public async Task<IActionResult> UpdateAsync(FeedbackDTO dto, CancellationToken cancellationToken = default)
    {
        var result = await _feedbackAtualizarService.UpdateAsync(dto, cancellationToken);
        return result.Convert(HttpStatusCode.BadRequest);
    }
}
