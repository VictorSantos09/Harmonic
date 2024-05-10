using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoGetRepository : Repository, IConteudoGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;

    public ConteudoGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                IDbConnection conn) : base(conn)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
    }

    public async Task<IEnumerable<ConteudoEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<ConteudoSnapshot> snapshots;

        snapshots = await _connection.QueryAsync<ConteudoSnapshot>(command);

        return snapshots.ToEntities<ConteudoEntity, ConteudoSnapshot, int>();
    }

    public async Task<ConteudoEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        ConteudoSnapshot? snapshot;
        snapshot = await _connection.QuerySingleOrDefaultAsync<ConteudoSnapshot>(command);

        return ConteudoEntity.FromSnapshot(snapshot);
    }
}
