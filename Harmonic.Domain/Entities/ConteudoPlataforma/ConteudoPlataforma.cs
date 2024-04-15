using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Shared.Exceptions;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.ConteudoPlataforma;

public class ConteudoPlataforma : IEntity<ConteudoPlataforma, ConteudoPlataformaSnapshot, int>
{
    public int Id { get; set; }
    public string URL { get; set; }
    public ConteudoEntity Conteudo { get; set; }
    public PlataformaEntity Plataforma { get; set; }

    public ConteudoPlataforma(string uRL, ConteudoEntity conteudo, PlataformaEntity plataforma)
    {
        URL = uRL;
        Conteudo = conteudo;
        Plataforma = plataforma;
    }

    public ConteudoPlataforma()
    {

    }

    public static ConteudoPlataforma? FromSnapshot(ConteudoPlataformaSnapshot? snapshot)
    {
        if (snapshot is null) return null;

        return new ConteudoPlataforma(
            snapshot.URL,
            ConteudoEntity.FromSnapshot(snapshot.ConteudoSnapshot) ?? throw new NullException<ConteudoPlataformaSnapshot>(),
            PlataformaEntity.FromSnapshot(snapshot.PlataformaSnapshot) ?? throw new NullException<PlataformaSnapshot>());
    }

    public ConteudoPlataformaSnapshot ToSnapshot()
    {
        throw new NotImplementedException();
    }
}
