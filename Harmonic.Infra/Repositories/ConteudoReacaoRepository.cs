using Dapper;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Shared.Data;
using System.Data;

namespace Harmonic.Infra.Repositories;

internal class ConteudoReacaoRepository : Repository, IConteudoReacaoRepository
{
    public ConteudoReacaoRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<int> AddAsync(ConteudoReacaoEntity entity, CancellationToken cancellationToken)
    {
        var sql = "INSERT INTO CONTEUDOS_REACOES(ID_CONTEUDO, ID_USUARIO, CURTIU) VALUES (idConteudo, idUsuario, curtiu)";

        CommandDefinition command = new(sql, new
        {
            entity.IdConteudo,
            entity.IdUsuario,
            entity.Curtiu
        }, cancellationToken: cancellationToken);

        return await _connection.ExecuteAsync(command);
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var sql = "DELETE FROM CONTEUDOS_REACOES WHERE ID = id";

        CommandDefinition command = new(sql, new { id }, cancellationToken: cancellationToken);

        return await _connection.ExecuteAsync(command);
    }

    public async Task<int> UpdateAsync(ConteudoReacaoEntity entity, CancellationToken cancellationToken)
    {
        var sql = "UPDATE CONTEUDOS_REACOES SET ID_CONTEUDO = idConteudo, ID_USUARIO = idUsuario, CURTIU = curtiu WHERE ID = id";

        CommandDefinition command = new(sql, new
        {
            entity.IdConteudo,
            entity.IdUsuario,
            entity.Curtiu
        }, cancellationToken: cancellationToken);

        return await _connection.ExecuteAsync(command);
    }

    public async Task<ConteudoReacaoEntity?> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM CONTEUDOS_REACOES WHERE ID_USUARIO = @idUsuario AND ID_CONTEUDO = @idConteudo";

        CommandDefinition command = new(sql, new
        {
            @idUsuario = idUsuario,
            @idConteudo = idConteudo
        }, cancellationToken: cancellationToken);

        return await _connection.QuerySingleOrDefaultAsync<ConteudoReacaoEntity>(command);
    }
}
