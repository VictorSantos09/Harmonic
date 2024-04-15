using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Plataforma;

public class PlataformaEntity : IEntity<PlataformaEntity, PlataformaSnapshot, int>
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string URL { get; set; }

    public PlataformaEntity(string nome, string url)
    {
        Nome = nome;
        URL = url;
    }

    public static PlataformaEntity? FromSnapshot(PlataformaSnapshot? snapshot)
    {
        if (snapshot is null) return null;
        return new(snapshot.NOME, snapshot.URL) { Id = snapshot.ID };
    }

    public PlataformaSnapshot ToSnapshot()
    {
        return new(Id, Nome, URL);
    }
}
