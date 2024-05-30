using Dapper;
using Harmonic.Domain.Entities.ConteudoReacao;
using Harmonic.Domain.Entities.ConteudoReacao.DTOs;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using System.Data;

namespace Harmonic.Infra.Repositories;

internal class ConteudoReacaoRepository : Repository, IConteudoReacaoRepository
{
    public ConteudoReacaoRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<int> AddAsync(ConteudoReacaoEntity entity, CancellationToken cancellationToken)
    {
        var sql = "INSERT INTO CONTEUDOS_REACOES(ID_CONTEUDO, ID_USUARIO, CURTIU) VALUES (@idConteudo, @idUsuario, @curtiu)";

        CommandDefinition command = new(sql, new
        {
            @idConteudo = entity.IdConteudo,
            @idUsuario = entity.IdUsuario,
            @curtiu = entity.Curtiu
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
        var sql = @"UPDATE CONTEUDOS_REACOES SET
                    ID_CONTEUDO = @idConteudo_param,
                    ID_USUARIO = @idUsuario_param,
                    CURTIU = @curtiu_param
                    WHERE ID = @id_param";

        CommandDefinition command = new(sql, new
        {
            @idConteudo_param = entity.IdConteudo,
            @idUsuario_param = entity.IdUsuario,
            @curtiu_param = entity.Curtiu,
            @id_param = entity.Id
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

        var snapshot = await _connection.QuerySingleOrDefaultAsync<ConteudoReacaoSnapshot>(command);

        return ConteudoReacaoEntity.FromSnapshot(snapshot);
    }

    public async Task<IEnumerable<ConteudoReacaoEntity>> GetUsuarioConteudoReacaoAsync(string idUsuario, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM CONTEUDOS_REACOES WHERE ID_USUARIO = @idUsuario";

        CommandDefinition command = new(sql, new
        {
            @idUsuario = idUsuario,
        }, cancellationToken: cancellationToken);

        var snapshots = await _connection.QueryAsync<ConteudoReacaoSnapshot>(command);

        return snapshots.ToEntities<ConteudoReacaoEntity, ConteudoReacaoSnapshot, int>();
    }

    public async Task<IEnumerable<UsuarioConteudoCurtidoDTO>> GetUsuarioConteudosCurtidosAsync(string idUsuario, CancellationToken cancellationToken)
    {
        var sql = @"SELECT C.ID
                    , C.TITULO
	                , C.IMAGEM
                    , P.NOME AS 'PAIS'
	                , P.ICON
                    ,TP.NOME AS 'TIPOCONTEUDO'
                    , CR.ID_CONTEUDO as 'IDCONTEUDO'
                FROM CONTEUDOS C
                JOIN PAISES P ON P.ID = C.ID_PAIS_ORIGEM
                JOIN TIPOS_CONTEUDOS TP ON TP.ID = C.ID_TIPO_CONTEUDO
                JOIN CONTEUDOS_REACOES CR ON CR.ID_CONTEUDO = C.ID
                WHERE CR.ID_USUARIO = @idUsuario
                AND CR.CURTIU = 1;";

        CommandDefinition command = new(sql, new
        {
            idUsuario
        }, cancellationToken: cancellationToken);

        return await _connection.QueryAsync<UsuarioConteudoCurtidoDTO>(command);
    }
}
