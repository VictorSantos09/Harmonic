using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
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

    public async Task<IFinal<ConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetByIdAsync(id, cancellationToken);

        if (result.IsNull()) return Final.Failure(result, "conteudo.getById.NaoEncontrado", "Não foi encontrado nenhum conteúdo");
        
        return Final.Success(result);
    }
}
