﻿using Dapper;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Regras.Contracts.Repositories.Pais;
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

    public PaisGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                            IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
    }

    public async Task<IEnumerable<PaisEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<PaisEntity>();

        CommandDefinition command = new(procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<PaisSnapshot> snapshots;
        using IDbConnection conn = Connect();
        snapshots = await conn.QueryAsync<PaisSnapshot>(command);

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
        using IDbConnection conn = Connect();
        result = await conn.QuerySingleOrDefaultAsync<PaisSnapshot>(command);

        return PaisEntity.FromSnapshot(result);
    }
}