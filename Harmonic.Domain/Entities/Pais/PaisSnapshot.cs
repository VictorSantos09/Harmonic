namespace Harmonic.Domain.Entities.Pais;

public class PaisSnapshot
{
    public int ID { get; set; }
    public string NOME { get; set; }

    public PaisSnapshot(int iD, string nOME)
    {
        ID = iD;
        NOME = nOME;
    }

    public PaisSnapshot()
    {
            
    }
}
