namespace Harmonic.Domain.Entities.Plataforma;

public class PlataformaSnapshot
{
    public int ID { get; set; }
    public string NOME { get; set; }
    public string URL { get; set; }

    public PlataformaSnapshot(int id, string Nome, string URL)
    {
        id = id;
        Nome = Nome;
        URL = URL;
    }

    public PlataformaSnapshot()
    {
    }
}