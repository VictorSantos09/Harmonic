using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoGetService : IConteudoGetService
{
    private readonly IConteudoGetRepository _conteudoGetRepository;

    public ConteudoGetService(IConteudoGetRepository conteudoGetRepository)
    {
        _conteudoGetRepository = conteudoGetRepository;
    }

    public async Task<IFinal<IEnumerable<ConteudoEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetAllAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<IEnumerable<ConteudoDetalhesDto>>> GetAllDetalhesAsync(CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetAllDetalhesAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<ConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetByIdAsync(id, cancellationToken);

        if (result.IsNull()) return Final.Failure(result, "conteudo.getById.NaoEncontrado", "Não foi encontrado nenhum conteúdo");
        
        return Final.Success(result);
    }

    public async Task<IFinal<IEnumerable<ConteudoTopEntity>>> GetTopRadiosAsync(CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetTopRadiosAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<IEnumerable<ConteudoTopEntity>>> GetTopPodcastsAsync(CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetTopPodcastsAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<ConteudoDetalhesDto>> GetDetalhesAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetDetalhesAsync(id, cancellationToken);

        if(result is null) return Final.Failure(result, "get.detalhes.notFound", $"conteudo com id {id} não encontrado");

        return Final.Success(result);
    }

    public async Task<IFinal<IEnumerable<string>>> GetConteudoPlataformasURL(int id, CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetConteudoPlataformasURL(id, cancellationToken);

        if (result.Count() == 0) return Final.Failure(result, "get.conteudoPlataformasURL.notFound", $"nenhuma plataforma do conteúdo com id {id} encontrada");

        return Final.Success(result);
    }
}
