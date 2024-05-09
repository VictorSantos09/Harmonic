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
    public int ID_FEEDBACK { get; set; }
    public int ID_PAIS { get; set; }
    public int ID_TIPO_CONTEUDO { get; set; }
    public TipoConteudoSnapshot TipoConteudo { get; set; }
    public PaisSnapshot Pais { get; set; }
    public FeedbackSnapshot Feedback { get; set; }

    public ConteudoSnapshot(int iD,
                            string titulo,
                            DateTime dATA_CADASTRO,
                            string dESCRICAO,
                            int iD_FEEDBACK,
                            int iD_PAIS,
                            int iD_TIPO_CONTEUDO,
                            TipoConteudoSnapshot tipoConteudo,
                            PaisSnapshot pais,
                            FeedbackSnapshot feedback)
    {
        ID = iD;
        TITULO = titulo;
        DATA_CADASTRO = dATA_CADASTRO;
        DESCRICAO = dESCRICAO;
        ID_FEEDBACK = iD_FEEDBACK;
        ID_PAIS = iD_PAIS;
        ID_TIPO_CONTEUDO = iD_TIPO_CONTEUDO;
        Feedback = feedback;
        TipoConteudo = tipoConteudo;
        Pais = pais;
    }

    public ConteudoSnapshot()
    {

    }
}
