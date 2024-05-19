using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoDeletarRepository : Repository, IConteudoDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public ConteudoDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy,
                                     IDbConnection conn) : base(conn)
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

        return _connection.ExecuteOnTransactionAsync(command);
    }

    public async Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        BeginTransaction();
        try
        {
            var procedureName = _procedureNameBuilderDeleteStrategy.Build<ConteudoEntity>();
            CommandDefinition command;
            foreach (var i in ids)
            {
                command = new(
                    procedureName, new
                    {
                        idParam = i
                    }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

                await _connection.ExecuteAsync(command);
            }
            Commit();
        }
        catch (Exception)
        {
            Rollback();
            throw;
        }
    }
}
