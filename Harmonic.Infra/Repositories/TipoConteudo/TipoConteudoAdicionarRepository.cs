using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.TipoConteudo
{
    internal class TipoConteudoAdicionarRepository : Repository, ITipoConteudoAdicionarRepository
    {

        private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
        private readonly IValidator<TipoConteudoEntity> _validator;

        public TipoConteudoAdicionarRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy,
                                               IValidator<TipoConteudoEntity> validator,
                                               IDbConnection conn) : base(conn)
        {
            _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
            _validator = validator;
        }

        public async Task<int> AddAsync(TipoConteudoEntity entity, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderAddStrategy.Build<TipoConteudoEntity>();

            object parameters = new
            {
                idParam = entity.Id,
                nomeParam = entity.Nome,
            };

            CommandDefinition command = new(procedureName,
                                            parameters,
                                            commandType: CommandType.StoredProcedure,
                                            cancellationToken: cancellationToken);

            return await _connection.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
        }
    }
}
