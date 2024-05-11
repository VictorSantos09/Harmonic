using Dapper;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;

namespace Harmonic.Infra.Repositories.Pais;

internal class PaisGetRepository : Repository, IPaisGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;

    public PaisGetRepository(IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                             IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                             IDbConnection conn) : base(conn)
    {
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
    }

    public async Task<IEnumerable<PaisEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<PaisEntity>();

        CommandDefinition command = new(procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<PaisSnapshot> snapshots;
        snapshots = await _connection.QueryAsync<PaisSnapshot>(command);

        return snapshots.ToEntities<PaisEntity, PaisSnapshot, int>();
    }

    public async Task<PaisEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<PaisEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                IdParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        PaisSnapshot? result;
        result = await _connection.QuerySingleOrDefaultAsync<PaisSnapshot>(command);

        return PaisEntity.FromSnapshot(result);
    }
}
