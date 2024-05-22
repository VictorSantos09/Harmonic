using Harmonic.Domain.Entities.Conteudo;
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
        ConteudoReacaoEntity entity = new()
        {
            IdConteudo = dto.IdConteudo,
            IdUsuario = dto.IdUsuario,
            Curtiu = dto.Curtiu
        };
        var result = await _conteudoReacaoRepository.AddAsync(entity, cancellationToken);

        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.add.Falha","Erro ao adicionar reação ao conteúdo");
    }

    public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.DeleteAsync(id, cancellationToken);
        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.delete.Falha","Erro ao deletar reação ao conteúdo");
    }

    public async Task<IFinal> UpdateAsync(ConteudoReacaoDTO dto, CancellationToken cancellationToken = default)
    {
        ConteudoReacaoEntity entity = new()
        {
            IdConteudo = dto.IdConteudo,
            IdUsuario = dto.IdUsuario,
            Curtiu = dto.Curtiu
        };

        var result = await _conteudoReacaoRepository.UpdateAsync(entity, cancellationToken);
        return result > 0 ? Final.Success() : Final.Failure("conteudoReacao.update.Falha","Erro ao atualizar reação ao conteúdo");
    }

    public async Task<IFinal<ConteudoReacaoEntity?>> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken = default)
    {
        var result = await _conteudoReacaoRepository.GetUsuarioConteudoReacaoAsync(idUsuario, idConteudo, cancellationToken);
        return result is null ? Final.Failure(result, "conteudoReacao.getUsuarioConteudoReacao.Falha","Nenhuma reação encontrada") : Final.Success(result);
    }
}
