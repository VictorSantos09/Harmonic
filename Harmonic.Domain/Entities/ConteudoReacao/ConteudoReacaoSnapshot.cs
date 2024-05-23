namespace Harmonic.Domain.Entities.ConteudoReacao;

public class ConteudoReacaoSnapshot
{
    public int ID { get; set; }
    public int ID_CONTEUDO { get; set; }
    public string ID_USUARIO { get; set; }
    public bool CURTIU { get; set; }
}