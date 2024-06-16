using Dapper;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;
using System.Threading;


namespace Harmonic.Infra.Repositories.Plataforma;

internal class PlataformaGetRepository : Repository, IPlataformaGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;

    public PlataformaGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                   IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                   IDbConnection conn) : base(conn)
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

        snapshots = await _connection.QueryAsync<PlataformaSnapshot>(command);

        return snapshots.ToEntities<PlataformaEntity, PlataformaSnapshot, int>();
    }

    public async Task<PlataformaEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<PlataformaEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        PlataformaSnapshot? result;
        result = await _connection.QuerySingleOrDefaultAsync<PlataformaSnapshot>(command);

        return PlataformaEntity.FromSnapshot(result);
    }

    public async Task<PlataformaEntity> GetByNameAsync(string nome, CancellationToken cancellationToken)
    {
        var procedureName = "SELECT * FROM PLATAFORMAS WHERE NOME = @nome";

        CommandDefinition command = new(
            procedureName, new
            {
                nome
            }, cancellationToken: cancellationToken);

        PlataformaSnapshot? result;
        result = await _connection.QuerySingleOrDefaultAsync<PlataformaSnapshot>(command);

        return PlataformaEntity.FromSnapshot(result);
    }
}
