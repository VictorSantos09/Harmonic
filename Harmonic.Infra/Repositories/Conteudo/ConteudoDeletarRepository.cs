using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoDeletarRepository : Repository, IConteudoDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public ConteudoDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy)
    {
        _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
    }

    public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderDeleteStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return conn.ExecuteOnTransactionAsync(command);
    }
}
