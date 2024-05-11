using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.Plataforma;

internal class PlataformaGetService : IPlataformaGetService
{
    private readonly IPlataformaGetRepository _plataformaGetRepository;

    public PlataformaGetService(IPlataformaGetRepository plataformaGetRepository)
    {
        _plataformaGetRepository = plataformaGetRepository;
    }

    public async Task<IFinal<IEnumerable<PlataformaEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _plataformaGetRepository.GetAllAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<PlataformaEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _plataformaGetRepository.GetByIdAsync(id, cancellationToken);

        if (result.IsNull()) return Final.Failure(result, "plataforma.getById.NaoEncontrado", "Não foi encontrado nenhum plataforma");

        return Final.Success(result);
    }
}
