using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Contracts.Conteudo;
using Harmonic.Infra.Repositories.Contracts.Plataforma;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;

namespace Harmonic.Infra.Repositories.Plataforma;

internal class PlataformaAtualizarRepository : Repository, IPlataformaAtualizarRepository
{
    private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
    private readonly IValidator<PlataformaEntity> _validator;

    public PlataformaAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                       IValidator<PlataformaEntity> validator, IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
        _validator = validator;
    }
    public async Task<int> UpdateAsync(PlataformaEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderUpdateStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = entity.Id,
                nomeParam = entity.Nome,
                urlParam = entity.URL
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return await conn.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
    }
}
