using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Shared.Exceptions;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.ConteudoPlataforma;

public class ConteudoPlataformaEntity : IEntity<int>
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

}
