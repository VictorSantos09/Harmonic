using Harmonic.Infra.Repositories.Contracts.Conteudo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmonic.Infra.Repositories.Contracts.Plataforma;
using Harmonic.Shared.Data;
using FluentValidation;
using QuickKit.Builders.ProcedureName.Add;
using Harmonic.Domain.Entities.Conteudo;
using Microsoft.Extensions.Configuration;
using Harmonic.Domain.Entities.Plataforma;
using Dapper;
using System.Data;
using QuickKit.Extensions;
using Harmonic.Shared.Constants.Base;


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
