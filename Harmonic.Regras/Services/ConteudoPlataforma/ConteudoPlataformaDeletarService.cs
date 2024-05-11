using Harmonic.Infra.Enums;
using Harmonic.Infra.Repositories.Common;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoPlataforma;

internal class ConteudoPlataformaDeletarService : IConteudoPlataformaDeletarService
{
    private readonly IConteudoPlataformaDeletarRepository _deletarRepository;
    private readonly IExistsRepository _existsRepository;

    public ConteudoPlataformaDeletarService(IConteudoPlataformaDeletarRepository deletarRepository, IExistsRepository existsRepository)
    {
        _deletarRepository = deletarRepository;
        _existsRepository = existsRepository;
    }

    public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.CONTEUDOS, id, cancellationToken);

        if (!exists) return Final.Failure("ConteudoPlataforma.Delete.NaoEncontrado", "Não foi encontrado");

        var result = await _deletarRepository.DeleteAsync(id, cancellationToken);

        if (result > 0) return Final.Success("Deletado com sucesso");

        return Final.Failure("ConteudoPlataforma.Delete.Falha", "Não foi possível deletar");
    }
}
