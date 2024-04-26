using Harmonic.Infra.Enums;
using Harmonic.Infra.Repositories.Contracts.Common;
using Harmonic.Infra.Repositories.Contracts.Plataforma;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;


namespace Harmonic.Regras.Services.Plataforma;

internal class PlataformaDeletarService : IPlataformaDeletarService
{
    private readonly IPlataformaDeletarRepository _plataformaDeletarRepository;
    private readonly IExistsRepository _existsRepository;

    public PlataformaDeletarService(IPlataformaDeletarRepository plataformaDeletarRepository, IExistsRepository existsRepository)
    {
        _plataformaDeletarRepository = plataformaDeletarRepository;
        _existsRepository = existsRepository;
    }

    public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.PLATAFORMAS, id, cancellationToken);

        if (!exists) return Final.Failure("plataforma.delete.NaoEncontrado", "O plataforma não foi encontrado");

        var result = await _plataformaDeletarRepository.DeleteAsync(id, cancellationToken);

        if (result > 0) return Final.Success("plataformadeletado com sucesso");

        return Final.Failure("plataforma.delete.Falha", "não foi possível deletar o plataforma");
    }
}
