using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.ConteudoReacao;
public class ConteudoReacaoEntity : IEntity<ConteudoReacaoEntity, ConteudoReacaoSnapshot, int>
{
    public int Id { get; set; }
    public int IdConteudo { get; set; }
    public string IdUsuario { get; set; }
    public bool Curtiu { get; set; }

    public static ConteudoReacaoEntity? FromSnapshot(ConteudoReacaoSnapshot? snapshot)
    {
       if (snapshot is null) return null;

        return new()
        {
            Id = snapshot.ID,
            Curtiu = snapshot.CURTIU,
            IdConteudo = snapshot.ID_CONTEUDO,
            IdUsuario = snapshot.ID_USUARIO
        };
    }

    public ConteudoReacaoSnapshot ToSnapshot()
    {
        return new()
        {
            CURTIU = Curtiu,
            ID_USUARIO = IdUsuario,
            ID_CONTEUDO = IdConteudo,
            ID = Id
        };
    }
}
