using Dapper;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Plataforma;

internal class PlataformaDeletarRepository : Repository, IPlataformaDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public PlataformaDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy,
                                       IDbConnection conn) : base(conn)
    {
        _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderDeleteStrategy.Build<PlataformaEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        return await _connection.ExecuteOnTransactionAsync(command);
    }
}
