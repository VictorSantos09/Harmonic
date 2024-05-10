using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma;

internal class ConteudoPlataformaGetRepository : Repository, IConteudoPlataformaGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;
    private readonly IValidator<ConteudoPlataformaEntity> _validator;
    private readonly IConteudoGetRepository _conteudoGetRepository;
    private readonly IPlataformaGetRepository _plataformaGetRepository;

    public ConteudoPlataformaGetRepository(IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy,
                                           IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy,
                                           IValidator<ConteudoPlataformaEntity> validator,
                                           IConteudoGetRepository conteudoGetRepository,
                                           IPlataformaGetRepository plataformaGetRepository,
                                           IDbConnection conn) : base(conn)
    {
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
        _validator = validator;
        _conteudoGetRepository = conteudoGetRepository;
        _plataformaGetRepository = plataformaGetRepository;
    }

    public async Task<IEnumerable<ConteudoPlataformaEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<ConteudoPlataformaEntity>();

        CommandDefinition command = new(
            procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<ConteudoPlataformaSnapshot> snapshots;

        snapshots = await _connection.QueryAsync<ConteudoPlataformaSnapshot>(command);

        List<ConteudoPlataformaEntity> output = [];
        foreach (var s in snapshots)
        {
            var conteudo = await _conteudoGetRepository.GetByIdAsync(s.ID_CONTEUDO, cancellationToken);
            var plataforma = await _plataformaGetRepository.GetByIdAsync(s.ID_PLATAFORMA, cancellationToken);

            output.Add(new ConteudoPlataformaEntity(s.URL, conteudo, plataforma));
        }

        return output;
    }

    public async Task<ConteudoPlataformaEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<ConteudoPlataformaEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        ConteudoPlataformaSnapshot? snapshot;
        snapshot = await _connection.QuerySingleOrDefaultAsync<ConteudoPlataformaSnapshot>(command);

        var conteudo = await _conteudoGetRepository.GetByIdAsync(snapshot.ID_CONTEUDO, cancellationToken);
        var plataforma = await _plataformaGetRepository.GetByIdAsync(snapshot.ID_PLATAFORMA, cancellationToken);

        return new ConteudoPlataformaEntity(snapshot.URL, conteudo, plataforma);
    }
}
