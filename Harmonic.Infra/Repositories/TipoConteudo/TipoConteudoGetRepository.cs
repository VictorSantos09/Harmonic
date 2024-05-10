using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
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

        public TipoConteudoGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                               IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                               IValidator<TipoConteudoEntity> validator,
                                               IDbConnection conn) : base(conn)
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

            snapshots = await _connection.QueryAsync<TipoConteudoSnapshot>(command);

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
            snapshot = await _connection.QuerySingleOrDefaultAsync<TipoConteudoSnapshot>(command);

            return TipoConteudoEntity.FromSnapshot(snapshot);
        }
    }
}
