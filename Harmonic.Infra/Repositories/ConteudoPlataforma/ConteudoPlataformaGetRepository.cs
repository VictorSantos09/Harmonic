using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma;

internal class ConteudoPlataformaGetRepository : Repository, IConteudoPlataformaGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;
    private readonly IValidator<ConteudoPlataformaEntity> _validator;

    public ConteudoPlataformaGetRepository(IConfiguration configuration,
                                           IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                           IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                           IValidator<ConteudoPlataformaEntity> validator) : base(configuration)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
        _validator = validator;
    }

    public async Task<IEnumerable<ConteudoPlataformaEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<ConteudoPlataformaEntity>();

        CommandDefinition command = new(
            procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<ConteudoPlataformaSnapshot> snapshots;

        using IDbConnection conn = Connect();
        snapshots = await conn.QueryAsync<ConteudoPlataformaSnapshot>(command);

        return snapshots.ToEntities<ConteudoPlataformaEntity, ConteudoPlataformaSnapshot, int>();
    }

    public async Task<ConteudoPlataformaEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<ConteudoPlataformaEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        ConteudoPlataformaSnapshot? snapshot;
        using IDbConnection conn = Connect();
        snapshot = await conn.QuerySingleOrDefaultAsync<ConteudoPlataformaSnapshot>(command);

        return ConteudoPlataformaEntity.FromSnapshot(snapshot);
    }
}
