using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.TipoConteudo;

public class TipoConteudoEntity : IEntity<TipoConteudoEntity, TipoConteudoSnapshot, int>
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public TipoConteudoEntity(string nome)
    {
        Nome = nome;
    }

    public TipoConteudoEntity(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public TipoConteudoEntity()
    {

    }

    public static TipoConteudoEntity? FromSnapshot(TipoConteudoSnapshot? snapshot)
    {
        if (snapshot is null) return null;
        return new(snapshot.NOME) { Id = snapshot.ID };
    }

    public TipoConteudoSnapshot ToSnapshot()
    {
        return new(Id, Nome);
    }
}