using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoAtualizarRepository : Repository, IConteudoAtualizarRepository
{
    private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
    private readonly IValidator<ConteudoEntity> _validator;

    public ConteudoAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                       IValidator<ConteudoEntity> validator,
                                       IDbConnection conn) : base(conn)
    {
        _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
        _validator = validator;
    }

    public async Task<int> UpdateAsync(ConteudoEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderUpdateStrategy.Build<ConteudoEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = entity.Id,
                titleParam = entity.Titulo,
                descricaoParam = entity.Descricao,
                idTipoConteudoParam = entity.TipoConteudo.Id,
                idPaisParam = entity.Pais.Id,
                imagemParam = entity.Imagem,
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        return await _connection.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
    }
}
