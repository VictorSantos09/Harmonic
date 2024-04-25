using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;
namespace Harmonic.Domain.Entities.ConteudoPlataforma;

public class ConteudoPlataformaSnapshot
{
    public int ID { get; set; }
    public string URL { get; set; } 
    public ConteudoSnapshot ConteudoSnapshot { get; set; } 
    public PlataformaSnapshot PlataformaSnapshot { get; set; }

    public ConteudoPlataformaSnapshot()
    {
        
    }

    public ConteudoPlataformaSnapshot(int iD, string uRL, ConteudoSnapshot conteudoSnapshot, PlataformaSnapshot plataformaSnapshot)
    {
        ID = iD;
        URL = uRL;
        ConteudoSnapshot = conteudoSnapshot;
        PlataformaSnapshot = plataformaSnapshot;
    }
}
