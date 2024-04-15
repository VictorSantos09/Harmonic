﻿using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoAtualizarRepository : Repository, IConteudoAtualizarRepository
{
    private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
    private readonly IValidator<ConteudoEntity> _validator;

    public ConteudoAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                       IValidator<ConteudoEntity> validator)
    {
        _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
        _validator = validator;
    }

    public async Task<int> UpdateAsync(ConteudoEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderUpdateStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = entity.Id,
                titleParam = entity.Titulo,
                dataCadastroParam = entity.DataCadastro,
                descricaoParam = entity.Descricao,
                idTipoConteudoParam = entity.TipoConteudo.Id,
                idPaisParam = entity.Pais.Id,
                idFeedbackParam = entity.Feedback.Id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return await conn.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
    }
}