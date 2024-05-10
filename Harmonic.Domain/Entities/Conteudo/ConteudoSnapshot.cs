using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;

namespace Harmonic.Domain.Entities.Conteudo;

public class ConteudoSnapshot
{
    public int ID { get; set; }
    public string TITULO { get; set; }
    public DateTime DATA_CADASTRO { get; set; }
    public string DESCRICAO { get; set; }
    public TipoConteudoSnapshot TipoConteudo { get; set; }
    public PaisSnapshot Pais { get; set; }
    public FeedbackSnapshot Feedback { get; set; }

    public ConteudoSnapshot(int iD,
                            string titulo,
                            DateTime dATA_CADASTRO,
                            string dESCRICAO,
                            TipoConteudoSnapshot tipoConteudo,
                            PaisSnapshot pais,
                            FeedbackSnapshot feedback)
    {
        ID = iD;
        TITULO = titulo;
        DATA_CADASTRO = dATA_CADASTRO;
        DESCRICAO = dESCRICAO;
        TipoConteudo = tipoConteudo;
        Pais = pais;
        Feedback = feedback;
    }

    public ConteudoSnapshot()
    {

    }
}
