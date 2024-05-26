using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Pais;

public class PaisEntity : IEntity<PaisEntity, PaisSnapshot, int>
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Icon { get; set; }

    public PaisEntity(string nome, string icon)
    {
        Nome = nome;
        Icon = icon;
    }

    public PaisEntity()
    {

    }

    public static PaisEntity? FromSnapshot(PaisSnapshot? snapshot)
    {
        if (snapshot is null) return null;
        return new(snapshot.NOME, snapshot.ICON) { Id = snapshot.ID };
    }

    public PaisSnapshot ToSnapshot()
    {
        return new(Id, Nome, Icon);
    }
}
