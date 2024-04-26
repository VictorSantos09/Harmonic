using Dapper;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Contracts.Plataforma;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;


namespace Harmonic.Infra.Repositories.Plataforma;

internal class PlataformaGetRepository : Repository, IPlataformaGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;

    public PlataformaGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy, IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
    }

    public async Task<IEnumerable<PlataformaEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<PlataformaEntity>();

        CommandDefinition command = new(
            procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<PlataformaSnapshot> snapshots;

        using IDbConnection conn = Connect();
        snapshots = await conn.QueryAsync<PlataformaSnapshot>(command);

        return snapshots.ToEntities<PlataformaEntity, PlataformaSnapshot, int>();
    }

    public Task<PlataformaEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
