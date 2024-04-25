using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Shared.Exceptions;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.ConteudoPlataforma;

public class ConteudoPlataformaEntity : IEntity<ConteudoPlataformaEntity, ConteudoPlataformaSnapshot, int>
{
    public int Id { get; set; }
    public string URL { get; set; }
    public ConteudoEntity Conteudo { get; set; }
    public PlataformaEntity Plataforma { get; set; }

    public ConteudoPlataformaEntity(string uRL, ConteudoEntity conteudo, PlataformaEntity plataforma)
    {
        URL = uRL;
        Conteudo = conteudo;
        Plataforma = plataforma;
    }

    public ConteudoPlataformaEntity()
    {

    }

    public static ConteudoPlataformaEntity? FromSnapshot(ConteudoPlataformaSnapshot? snapshot)
    {
        if (snapshot is null) return null;

        return new ConteudoPlataformaEntity(
            snapshot.URL,
            ConteudoEntity.FromSnapshot(snapshot.ConteudoSnapshot) ?? throw new SnapshotNullException<ConteudoPlataformaSnapshot>(),
            PlataformaEntity.FromSnapshot(snapshot.PlataformaSnapshot) ?? throw new SnapshotNullException<PlataformaSnapshot>());
    }

    public ConteudoPlataformaSnapshot ToSnapshot()
    {
        return new(Id, URL, Conteudo.ToSnapshot(), Plataforma.ToSnapshot());
    }
}
