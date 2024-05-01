using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.ConteudoPlataforma;

internal class ConteudoPlataformaGetService : IConteudoPlataformaGetService
{
    private readonly IConteudoPlataformaGetRepository _getRepository;

    public ConteudoPlataformaGetService(IConteudoPlataformaGetRepository conteudoGetRepository)
    {
        _getRepository = conteudoGetRepository;
    }

    public async Task<IFinal<IEnumerable<ConteudoPlataformaEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _getRepository.GetAllAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<ConteudoPlataformaEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _getRepository.GetByIdAsync(id, cancellationToken);

        if (result.IsNull()) return Final.Failure(result, "ConteudoPlataforma.getById.NaoEncontrado", "Não foi encontrado nenhum conteúdo");

        return Final.Success(result);
    }
}
