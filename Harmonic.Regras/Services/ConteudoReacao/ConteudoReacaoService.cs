using Harmonic.Domain.Entities.ConteudoReacao;
using Harmonic.Domain.Entities.ConteudoReacao.DTOs;
using Harmonic.Infra.Repositories;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Regras.Services.ConteudoReacao.DTO;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoReacao;

internal class ConteudoReacaoService : IConteudoReacaoService
{
    private readonly IConteudoReacaoRepository _conteudoReacaoRepository;
    private readonly IFeedbackAtualizarRepository _feedbackAtualizarRepository;
    private readonly IFeedbackGetRepository _feedbackGetRepository;
    private readonly IConteudoGetRepository _conteudoGetRepository;

    public ConteudoReacaoService(IConteudoReacaoRepository conteudoReacaoRepository,
                                 IFeedbackAtualizarRepository feedbackAtualizarRepository,
                                 IFeedbackGetRepository feedbackGetRepository,
                                 IConteudoGetRepository conteudoGetRepository)
    {
        _conteudoReacaoRepository = conteudoReacaoRepository;
        _feedbackAtualizarRepository = feedbackAtualizarRepository;
        _feedbackGetRepository = feedbackGetRepository;
        _conteudoGetRepository = conteudoGetRepository;
    }

    public async Task<IFinal> AddAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        var usuarioReacaoEncontrado = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(dto.IdUsuario,
                                                                                                    dto.IdConteudo,
                                                                                                    cancellationToken);

        if (usuarioReacaoEncontrado is not null)
        {
            if (usuarioReacaoEncontrado.IdUsuario == dto.IdUsuario)
            {
                await UpdateAsync(dto, cancellationToken);
                return Final.Success("Reação atualizada com sucesso");
            }

            else return Final.Failure("conteudoReacao.add.ReacaoExiste", "Já existe uma reação para esse conteúdo");
        }

        ConteudoReacaoEntity entity = new()
        {
            IdConteudo = dto.IdConteudo,
            IdUsuario = dto.IdUsuario,
            Curtiu = dto.Curtiu
        };

        var conteudoEncontrado = await _conteudoGetRepository.GetByIdAsync(dto.IdConteudo, cancellationToken);

        if (conteudoEncontrado is null) return Final.Failure("conteudoReacao.add.ConteudoNaoEncontrado", "Conteúdo não encontrado");

        conteudoEncontrado.Feedback.TotalCurtidas += 1;

        if (dto.Curtiu)
        {
            conteudoEncontrado.Feedback.TotalGosteis += 1;
        }

        var resultReacao = await _conteudoReacaoRepository.AddAsync(entity, cancellationToken);
        var resultFeedback = await _feedbackAtualizarRepository.UpdateAsync(conteudoEncontrado.Feedback, cancellationToken);

        if (resultFeedback > 0 && resultReacao > 0)
        {
            return Final.Success();
        }

        return Final.Failure("conteudoReacao.add.Falha", "Erro ao adicionar reação ao conteúdo");
    }

    public async Task<IFinal> DeleteAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken = default)
    {
        var conteudoReacaoEncontrado = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);

        if (conteudoReacaoEncontrado is null) return Final.Failure("conteudoReacao.delete.ReacaoNaoEncontrada", "Não foi encontrada nenhuma reação do usuário para esse conteúdo");

        var result = await _conteudoReacaoRepository.DeleteAsync(conteudoReacaoEncontrado.Id, cancellationToken);
        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.delete.Falha", "Erro ao deletar reação ao conteúdo");
    }

    public async Task<IFinal> UpdateAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        var usuarioReacaoEncontrado = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(dto.IdUsuario,
                                                                                                 dto.IdConteudo,
                                                                                                 cancellationToken);

        if (usuarioReacaoEncontrado is null) return Final.Failure("conteudoReacao.add.ReacaoNaoExiste", "Reação do conteúdo não encontrada");

        var conteudoEncontrado = await _conteudoGetRepository.GetByIdAsync(usuarioReacaoEncontrado.IdConteudo, cancellationToken);

        if (conteudoEncontrado is null) return Final.Failure("conteudoReacao.update.ConteudoNaoEncontrado", "Conteúdo não encontrado");

        usuarioReacaoEncontrado.IdConteudo = dto.IdConteudo;
        usuarioReacaoEncontrado.IdUsuario = dto.IdUsuario;
        usuarioReacaoEncontrado.Curtiu = dto.Curtiu;

        if (dto.Curtiu)
        {
            conteudoEncontrado.Feedback.TotalGosteis += 1;
        }
        else
        {
            conteudoEncontrado.Feedback.TotalGosteis -= 1;
        }

        var resultFeedback = await _feedbackAtualizarRepository.UpdateAsync(conteudoEncontrado.Feedback, cancellationToken);
        var resultConteudo = await _conteudoReacaoRepository.UpdateAsync(usuarioReacaoEncontrado, cancellationToken);

        if (resultFeedback > 0 && resultConteudo > 0)
            return Final.Success();

        return Final.Failure("conteudoReacao.update.Falha", "Erro ao atualizar reação ao conteúdo");
    }

    public async Task<IFinal<bool>> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);
        return result is null ? Final.Failure(false, "conteudoReacao.getUsuarioConteudoReacao.Falha", "Nenhuma reação encontrada") : Final.Success(result.Curtiu);
    }

    public async Task<IFinal<IEnumerable<ConteudoReacaoEntity>>> GetUsuarioConteudoReacaoAsync(string idUsuario, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, cancellationToken);

        return Final.Success(result);
    }

    public async Task<IFinal<IEnumerable<UsuarioConteudoCurtidoDTO>>> GetUsuarioConteudosCurtidosAsync(string idUsuario, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.GetUsuarioConteudosCurtidosAsync(idUsuario, cancellationToken);
        return Final.Success(result);
    }
}
