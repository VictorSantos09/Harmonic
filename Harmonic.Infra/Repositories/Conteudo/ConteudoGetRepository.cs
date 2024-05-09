using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;
using ZstdSharp.Unsafe;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoGetRepository : Repository, IConteudoGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;
    private readonly ITipoConteudoGetRepository _tipoConteudoRepository;
    private readonly IFeedbackGetRepository _feedbackRepository;
    private readonly IPaisGetRepository _paisRepository;

    public ConteudoGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy, ITipoConteudoGetRepository tipoConteudoRepository,
                                IFeedbackGetRepository feedbackRepository, IPaisGetRepository paisRepository,
                                IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
        _tipoConteudoRepository = tipoConteudoRepository;
        _feedbackRepository = feedbackRepository;
        _paisRepository = paisRepository;
    }


    public async Task<IEnumerable<ConteudoEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<ConteudoSnapshot> snapshots;

        using IDbConnection conn = Connect();
        snapshots = await conn.QueryAsync<ConteudoSnapshot>(command);

        foreach (var snapshot in snapshots)
        {
            TipoConteudoEntity tipoConteudo = await _tipoConteudoRepository.GetByIdAsync(snapshot.ID_TIPO_CONTEUDO, cancellationToken);
            snapshot.TipoConteudo = tipoConteudo.ToSnapshot();
            FeedbackEntity feedback = await _feedbackRepository.GetByIdAsync(snapshot.ID_FEEDBACK, cancellationToken);
            snapshot.Feedback = feedback.ToSnapshot();
            PaisEntity pais = await _paisRepository.GetByIdAsync(snapshot.ID_PAIS, cancellationToken);
            snapshot.Pais = pais.ToSnapshot();        }

        return snapshots.ToEntities<ConteudoEntity, ConteudoSnapshot, int>();
    }

    public async Task<ConteudoEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        ConteudoSnapshot? snapshot;
        using IDbConnection conn = Connect();

        snapshot = await conn.QuerySingleOrDefaultAsync<ConteudoSnapshot>(command);

        TipoConteudoEntity tipoConteudo = await _tipoConteudoRepository.GetByIdAsync(snapshot.ID_TIPO_CONTEUDO, cancellationToken);
        snapshot.TipoConteudo = tipoConteudo.ToSnapshot();
        FeedbackEntity feedback = await  _feedbackRepository.GetByIdAsync(snapshot.ID_FEEDBACK, cancellationToken);
        snapshot.Feedback = feedback.ToSnapshot();
        PaisEntity pais = await _paisRepository.GetByIdAsync(snapshot.ID_PAIS, cancellationToken);
        snapshot.Pais = pais.ToSnapshot();    

        return ConteudoEntity.FromSnapshot(snapshot);
    }
}
