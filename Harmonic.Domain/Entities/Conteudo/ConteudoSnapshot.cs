namespace Harmonic.Domain.Entities.Conteudo;

public class ConteudoSnapshot
{
    public int ID { get; set; }
    public string TITULO { get; set; }
    public DateTime DATA_CADASTRO { get; set; }
    public string DESCRICAO { get; set; }
    public int ID_TIPO_CONTEUDO { get; set; }
    public int ID_PAIS_ORIGEM { get; set; }
    public int ID_FEEDBACK { get; set; }
}
