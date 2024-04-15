using Harmonic.Regras.Contracts.Repositories.Common;
using Harmonic.Regras.Contracts.Repositories.Pais;
using Harmonic.Regras.Enums;
using Harmonic.Regras.Services.Pais.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Pais;

internal class PaisDeletarService : IPaisDeletarService
{
    private readonly IPaisDeletarRepository _paisDeletarRepository;
    private readonly IExistsRepository _existsRepository;

    public PaisDeletarService(IPaisDeletarRepository paisDeletarRepository,
                              IExistsRepository existsRepository)
    {
        _paisDeletarRepository = paisDeletarRepository;
        _existsRepository = existsRepository;
    }

    public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.PAISES, id, cancellationToken);

        if (!exists) return Final.Failure("pais.deletar.NaoExiste", "o pais não foi encontrado");

        var result = await _paisDeletarRepository.DeleteAsync(id, cancellationToken);

        if (result > 0) return Final.Success("pais deletado com sucesso");

        return Final.Failure("pais.deletar.Falha", "não foi possível deletar o país");
    }
}
