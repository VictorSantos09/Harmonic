using Harmonic.Regras.Contracts.Repositories.Common;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
using Harmonic.Regras.Enums;
using Harmonic.Regras.Services.Conteudo.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoDeletarService : IConteudoDeletarService
{
    private readonly IConteudoDeletarRepository _conteudoDeletarRepository;
    private readonly IExistsRepository _existsRepository;

    public ConteudoDeletarService(IConteudoDeletarRepository conteudoDeletarRepository, IExistsRepository existsRepository)
    {
        _conteudoDeletarRepository = conteudoDeletarRepository;
        _existsRepository = existsRepository;
    }

    public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.CONTEUDOS, id, cancellationToken);

        if (!exists) return Final.Failure("conteudo.delete.NaoEncontrado", "O conteúdo não foi encontrado");

        var result = await _conteudoDeletarRepository.DeleteAsync(id, cancellationToken);

        if (result > 0) return Final.Success("conteúdo deletado com sucesso");

        return Final.Failure("conteudo.delete.Falha", "não foi possível deletar o conteúdo");
    }
}
