
namespace Harmonic.Domain.Entities.TipoConteudo;

public class TipoConteudoSnapshot
{
    public int ID { get; set; }
    public string NOME { get; set; }

    
    public TipoConteudoSnapshot()
    {
    }

    public TipoConteudoSnapshot(int iD, string nOME)
    {
        ID = iD;
        NOME = nOME;
    }

    public static implicit operator TipoConteudoSnapshot?(TipoConteudoEntity? v)
    {
        throw new NotImplementedException();
    }
}
