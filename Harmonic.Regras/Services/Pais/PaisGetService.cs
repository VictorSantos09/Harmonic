using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Regras.Services.Pais.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.Pais;

internal class PaisGetService : IPaisGetService
{
    private readonly IPaisGetRepository _paisGetRepository;

    public PaisGetService(IPaisGetRepository paisGetRepository)
    {
        _paisGetRepository = paisGetRepository;
    }

    public async Task<IFinal<IEnumerable<PaisEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _paisGetRepository.GetAllAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<PaisEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _paisGetRepository.GetByIdAsync(id, cancellationToken);
        if (result.IsNull()) return Final.Failure(result, "pais.get.NaoEncontrado", "O país não foi encontrado");

        return Final.Success(result);
    }
}
