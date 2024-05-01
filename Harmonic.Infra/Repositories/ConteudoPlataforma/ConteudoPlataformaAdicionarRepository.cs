using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;


namespace Harmonic.Infra.Repositories.ConteudoPlataforma
{
    internal class ConteudoPlataformaAdicionarRepository : Repository, IConteudoPlataformaAdicionarRepository
    {
        private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
        private readonly IValidator<ConteudoPlataformaEntity> _validator;

        public ConteudoPlataformaAdicionarRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy,
                                                     IValidator<ConteudoPlataformaEntity> validator,
                                                     IConfiguration configuration) : base(configuration)
        {
            _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
            _validator = validator;
        }
        public async Task<int> AddAsync(ConteudoPlataformaEntity entity, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderAddStrategy.Build<ConteudoPlataformaEntity>();

            object parameters = new
            {
                urlParam = entity.URL,
                idConteudoParam = entity.Conteudo.Id,
                idPlataformaParam =  entity.Plataforma.Id,
            };

            CommandDefinition command = new(procedureName,
                                            parameters,
                                            commandType: CommandType.StoredProcedure,
                                            cancellationToken: cancellationToken);

            using (IDbConnection conn = Connect())
            {
                var validationResult = await _validator.ValidateAsync(entity, cancellationToken);
                return await conn.ExecuteValidatingAsync(entity, _validator, "O ConteudoPlataforma é inválido", command);
            }
        }
    }
}
