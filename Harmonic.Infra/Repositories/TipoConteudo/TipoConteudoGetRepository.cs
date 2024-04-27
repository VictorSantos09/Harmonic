using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Contracts.TipoConteudo;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;

namespace Harmonic.Infra.Repositories.TipoConteudo
{
    internal class TipoConteudoGetRepository : Repository, ITipoConteudoGetRepository
    {
        private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
        private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;
        private readonly IValidator<TipoConteudoEntity> _validator;

        public TipoConteudoGetRepository(IConfiguration configuration,
                                               IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                               IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                               IValidator<TipoConteudoEntity> validator) : base(configuration)
        {
            _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
            _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
            _validator = validator;
        }

        public async Task<IEnumerable<TipoConteudoEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderGetAllStrategy.Build<TipoConteudoEntity>();

            CommandDefinition command = new(
                procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

            IEnumerable<TipoConteudoSnapshot> snapshots;

            using IDbConnection conn = Connect();
            snapshots = await conn.QueryAsync<TipoConteudoSnapshot>(command);

            return snapshots.ToEntities<TipoConteudoEntity, TipoConteudoSnapshot, int>();
        }

        public async Task<TipoConteudoEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var procedureName = _procedureNameBuilderGetByIdStrategy.Build<TipoConteudoEntity>();

            CommandDefinition command = new(
                procedureName, new
                {
                    idParam = id
                }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

            TipoConteudoSnapshot? snapshot;
            using IDbConnection conn = Connect();
            snapshot = await conn.QuerySingleOrDefaultAsync<TipoConteudoSnapshot>(command);

            return TipoConteudoEntity.FromSnapshot(snapshot);
        }
    }
}
