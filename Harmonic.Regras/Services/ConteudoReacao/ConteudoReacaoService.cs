using Harmonic.Domain.Entities.ConteudoReacao;
using Harmonic.Infra.Repositories;
using Harmonic.Regras.Services.ConteudoReacao.DTO;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoReacao;

internal class ConteudoReacaoService : IConteudoReacaoService
{
    private readonly IConteudoReacaoRepository _conteudoReacaoRepository;

    public ConteudoReacaoService(IConteudoReacaoRepository conteudoReacaoRepository)
    {
        _conteudoReacaoRepository = conteudoReacaoRepository;
    }

    public async Task<IFinal> AddAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        var usuarioReacaoEncontrado = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(dto.IdUsuario,
                                                                                                    dto.IdConteudo,
                                                                                                    cancellationToken);

        if (usuarioReacaoEncontrado is not null) return Final.Failure("conteudoReacao.add.ReacaoExiste", "Já existe uma reação para esse conteúdo");

        ConteudoReacaoEntity entity = new()
        {
            IdConteudo = dto.IdConteudo,
            IdUsuario = dto.IdUsuario,
            Curtiu = dto.Curtiu
        };
        var result = await _conteudoReacaoRepository.AddAsync(entity, cancellationToken);

        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.add.Falha","Erro ao adicionar reação ao conteúdo");
    }

    public async Task<IFinal> DeleteAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken = default)
    {
        var conteudoReacaoEncontrado = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);

        if (conteudoReacaoEncontrado is null) return Final.Failure("conteudoReacao.delete.ReacaoNaoEncontrada", "Não foi encontrada nenhuma reação do usuário para esse conteúdo");

        var result = await _conteudoReacaoRepository.DeleteAsync(conteudoReacaoEncontrado.Id, cancellationToken);
        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.delete.Falha","Erro ao deletar reação ao conteúdo");
    }

    public async Task<IFinal> UpdateAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        var usuarioReacaoEncontrado = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(dto.IdUsuario,
                                                                                                 dto.IdConteudo,
                                                                                                 cancellationToken);

        if (usuarioReacaoEncontrado is null) return Final.Failure("conteudoReacao.add.ReacaoNaoExiste", "Reação do coteúdo não encontrada");

        ConteudoReacaoEntity entity = new()
        {
            IdConteudo = dto.IdConteudo,
            IdUsuario = dto.IdUsuario,
            Curtiu = dto.Curtiu
        };

        var result = await _conteudoReacaoRepository.UpdateAsync(entity, cancellationToken);
        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.update.Falha","Erro ao atualizar reação ao conteúdo");
    }

    public async Task<IFinal<bool>> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);
        return result is null ? Final.Failure(false, "conteudoReacao.getUsuarioConteudoReacao.Falha","Nenhuma reação encontrada") : Final.Success(result.Curtiu);
    }

    public async Task<IFinal<IEnumerable<ConteudoReacaoEntity>>> GetUsuarioConteudoReacaoAsync(string idUsuario, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, cancellationToken);

        return Final.Success(result);
    }
}
