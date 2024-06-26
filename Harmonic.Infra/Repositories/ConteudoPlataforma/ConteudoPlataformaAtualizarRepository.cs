﻿using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma
{
    internal class ConteudoPlataformaAtualizarRepository : Repository, IConteudoPlataformaAtualizarRepository
    {
        private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
        private readonly IValidator<ConteudoPlataformaEntity> _validator;

        public ConteudoPlataformaAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                                     IValidator<ConteudoPlataformaEntity> validator,
                                                     IDbConnection conn) : base(conn)
        {
            _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
            _validator = validator;
        }

        public async Task<int> UpdateAsync(ConteudoPlataformaEntity entity, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderUpdateStrategy.Build<ConteudoPlataformaEntity>();

            CommandDefinition command = new(
                procedureName, new
                {
                    idParam = entity.Id,
                    urlParam = entity.URL,
                    idConteudoParam = entity.Conteudo.Id,
                    idPlataformaParam = entity.Plataforma.Id,
                }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

            return await _connection.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);

        }
    }
}
