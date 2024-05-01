using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.TipoConteudo
{
    internal class TipoConteudoDeletarRepository : Repository, ITipoConteudoDeletarRepository
    {
        private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

        public TipoConteudoDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy, IConfiguration configuration) : base(configuration)
        {
            _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
        }

        public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderDeleteStrategy.Build<TipoConteudoEntity>();

            CommandDefinition command = new(
                procedureName, new
                {
                    idParam = id
                }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

            using IDbConnection conn = Connect();
            return await conn.ExecuteOnTransactionAsync(command);
        }
    }
}
