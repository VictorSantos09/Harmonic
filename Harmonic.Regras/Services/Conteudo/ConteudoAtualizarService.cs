using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Feedback.Contracts;
using Harmonic.Regras.Services.Pais.Contracts;
using QuickKit.ResultTypes;
using System.Transactions;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAtualizarService : IConteudoAtualizarService
{
    private readonly IConteudoGetRepository _conteudoGetRepository;
    private readonly IConteudoAtualizarRepository _conteudoAtualizarRepository;
    private readonly ITipoConteudoGetRepository _tipoConteudoGetRepository;
    private readonly IPaisGetRepository _paisGetRepository;
    private readonly IFeedbackGetRepository _feedbackGetRepository;

    public ConteudoAtualizarService(IConteudoGetRepository conteudoGetRepository,
                                    IConteudoAtualizarRepository conteudoAtualizarRepository,
                                    ITipoConteudoGetRepository tipoConteudoGetRepository,
                                    IPaisGetRepository paisGetRepository,
                                    IFeedbackGetRepository feedbackGetRepository)
    {
        _conteudoGetRepository = conteudoGetRepository;
        _conteudoAtualizarRepository = conteudoAtualizarRepository;
        _tipoConteudoGetRepository = tipoConteudoGetRepository;
        _paisGetRepository = paisGetRepository;
        _feedbackGetRepository = feedbackGetRepository;
    }

    public async Task<IFinal> UpdateAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        var conteudo = await _conteudoGetRepository.GetByIdAsync(dto.Id, cancellationToken);
        var tipoConteudo = await _tipoConteudoGetRepository.GetByIdAsync(dto.IdTipoConteudo, cancellationToken);
        var pais = await _paisGetRepository.GetByIdAsync(dto.IdPais, cancellationToken);

        if (conteudo is null) return Final.Failure("conteudo.update.NotFound", $"conteúdo com id {dto.Id} não encontrado");
        if (tipoConteudo is null) return Final.Failure("conteudo.update.NotFound", $"tipo conteudo com id {dto.IdTipoConteudo} não encontrado");
        if (pais is null) return Final.Failure("conteudo.update.NotFound", $"país com id {dto.IdPais} não encontrado");


        conteudo.Imagem = dto.Imagem is null ? ConteudoAdicionarService.DEFAULT_IMAGE : dto.Imagem;
        conteudo.Titulo = dto.Titulo;
        conteudo.Descricao = dto.Descricao;
        conteudo.Pais = pais;
        conteudo.TipoConteudo = tipoConteudo;

        var result = await _conteudoAtualizarRepository.UpdateAsync(conteudo, cancellationToken);

        if(result == 0) return Final.Failure("conteudo.update.sucess", "conteudo atualizado com sucesso");

        return Final.Success("conteudo.update.ok");
    }
}
