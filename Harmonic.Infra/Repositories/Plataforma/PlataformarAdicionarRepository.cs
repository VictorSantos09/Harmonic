﻿using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;


namespace Harmonic.Infra.Repositories.Plataforma;

internal class PlataformarAdicionarRepository : Repository, IPlataformaAdicionarRepository
{
    private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
    private readonly IValidator<PlataformaEntity> _validator;

    public PlataformarAdicionarRepository(IConfiguration configuration, IValidator<PlataformaEntity> validator, IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy) : base(configuration)
    {
        _validator = validator;
        _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
    }

    public async Task<int> AddAsync(PlataformaEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderAddStrategy.Build<PlataformaEntity>();

        object parameters = new
        {
            nomeParam = entity.Nome,
            urlParam = entity.URL
        };

        CommandDefinition command = new(procedureName,
                                        parameters,
                                        commandType: CommandType.StoredProcedure,
                                        cancellationToken: cancellationToken);

        using (IDbConnection conn = Connect())
        {
            return await conn.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
        }
    }
}
