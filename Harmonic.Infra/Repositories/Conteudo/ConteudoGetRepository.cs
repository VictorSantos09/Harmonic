using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Exceptions;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoGetRepository : Repository, IConteudoGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;
    private readonly ITipoConteudoGetRepository _tipoConteudoGetRepository;
    private readonly IPaisGetRepository _paisGetRepository;
    private readonly IFeedbackGetRepository _feedbackGetRepository;

    public ConteudoGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                 IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                 ITipoConteudoGetRepository tipoConteudoGetRepository,
                                 IPaisGetRepository paisGetRepository,
                                 IFeedbackGetRepository feedbackGetRepository,
                                 IDbConnection conn) : base(conn)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
        _tipoConteudoGetRepository = tipoConteudoGetRepository;
        _paisGetRepository = paisGetRepository;
        _feedbackGetRepository = feedbackGetRepository;
    }

    public async Task<IEnumerable<ConteudoEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        var snapshots = await _connection.QueryAsync<ConteudoSnapshot>(command);

        List<ConteudoEntity> output = [];
        ConteudoEntity conteudo;

        foreach (var snapshot in snapshots)
        {
            var tipoConteudo = await _tipoConteudoGetRepository.GetByIdAsync(snapshot.ID_FEEDBACK, cancellationToken);
            var pais = await _paisGetRepository.GetByIdAsync(snapshot.ID_PAIS_ORIGEM, cancellationToken);
            var feedback = await _feedbackGetRepository.GetByIdAsync(snapshot.ID_FEEDBACK, cancellationToken);

            conteudo = new ConteudoEntity(snapshot.TITULO, snapshot.DATA_CADASTRO, snapshot.DESCRICAO, tipoConteudo, pais, feedback);
            output.Add(conteudo);
        }

        return output;
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
        snapshot = await _connection.QuerySingleOrDefaultAsync<ConteudoSnapshot>(command);

        if (snapshot is null) return null;

        var tipoConteudo = await _tipoConteudoGetRepository.GetByIdAsync(snapshot.ID_TIPO_CONTEUDO, cancellationToken);
        var pais = await _paisGetRepository.GetByIdAsync(snapshot.ID_PAIS_ORIGEM, cancellationToken);
        var feedback = await _feedbackGetRepository.GetByIdAsync(snapshot.ID_FEEDBACK, cancellationToken);

        if (tipoConteudo is null) throw new NotFoundException($"tipo conteudo com id {snapshot.ID_TIPO_CONTEUDO} não encontrado");
        if (pais is null) throw new NotFoundException($"país com id {snapshot.ID_PAIS_ORIGEM} não encontrado");
        if (feedback is null) throw new NotFoundException($"feedback com id {snapshot.ID_FEEDBACK} não encontrado");

        return new ConteudoEntity(snapshot.TITULO, snapshot.DATA_CADASTRO, snapshot.DESCRICAO, tipoConteudo, pais, feedback) { Id = snapshot.ID };
    }
}
