using Dapper;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma
{
    internal class ConteudoPlataformaDeletarRepository : Repository, IConteudoPlataformaDeletarRepository
    {
        private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

        public ConteudoPlataformaDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy,
                                                   IDbConnection conn) : base(conn)
        {
            _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
        }

        public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderDeleteStrategy.Build<ConteudoPlataformaEntity>();

            CommandDefinition command = new(
                procedureName, new
                {
                    idParam = id
                }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

            return _connection.ExecuteOnTransactionAsync(command);
        }
    }
}

    
