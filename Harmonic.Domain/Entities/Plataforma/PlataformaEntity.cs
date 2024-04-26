using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Plataforma;

public class PlataformaEntity : IEntity<PlataformaEntity, PlataformaSnapshot, int>
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string URL { get; set; }

    public PlataformaEntity(int id, string nome, string uRL)
    {
        Id = id;
        Nome = nome;
        URL = uRL;
    }

    public static PlataformaEntity? FromSnapshot(PlataformaSnapshot? snapshot)
    {
        if (snapshot is null) return null;
        return new PlataformaEntity(snapshot.ID,snapshot.NOME,snapshot.URL);
    }

    public PlataformaSnapshot ToSnapshot()
    {
        return new(Id, Nome, URL);
    }
}
