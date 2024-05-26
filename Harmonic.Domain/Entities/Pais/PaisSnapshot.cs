namespace Harmonic.Domain.Entities.Pais;

public class PaisSnapshot
{
    public int ID { get; set; }
    public string NOME { get; set; }
    public string ICON { get; set; }

    public PaisSnapshot(int iD, string nOME, string iCON)
    {
        ID = iD;
        NOME = nOME;
        ICON = iCON;
    }

    public PaisSnapshot()
    {
            
    }
}
