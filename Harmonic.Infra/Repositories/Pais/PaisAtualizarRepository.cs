﻿using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Regras.Contracts.Repositories.Pais;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Pais;

internal class PaisAtualizarRepository : Repository, IPaisAtualizarRepository
{
    private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
    private readonly IValidator<PaisEntity> _validator;

    public PaisAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                    IValidator<PaisEntity> validator)
    {
        _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
        _validator = validator;
    }

    public async Task<int> UpdateAsync(PaisEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderUpdateStrategy.Build<PaisEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                IdParam = entity.Id,
                nomeParam = entity.Nome
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return await conn.ExecuteValidatingAsync(entity, _validator, "O pais é inválido", command);
    }
}