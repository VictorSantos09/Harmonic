using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.TipoConteudo
{
    internal class TipoConteudoAtualizarRepository : Repository, ITipoConteudoAtualizarRepository
    {
        private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
        private readonly IValidator<TipoConteudoEntity> _validator;

        public TipoConteudoAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                           IValidator<TipoConteudoEntity> validator, IConfiguration configuration) : base(configuration)
        {
            _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
            _validator = validator;
        }

        public async Task<int> UpdateAsync(TipoConteudoEntity entity, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderUpdateStrategy.Build<TipoConteudoEntity>();

            CommandDefinition command = new(
                procedureName, new
                {
                    idParam = entity.Id,
                    nomeParam = entity.Nome,

                }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

            using IDbConnection conn = Connect();
            return await conn.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
        }
    }
}
