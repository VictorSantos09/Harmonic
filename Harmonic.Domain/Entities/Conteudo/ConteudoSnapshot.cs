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
    public TipoConteudoEntity TipoConteudo { get; set; }
    public PaisEntity Pais { get; set; }
    public FeedbackEntity Feedback { get; set; }

    public ConteudoSnapshot(int iD,
        string titulo,
                            DateTime dATA_CADASTRO,
                            string dESCRICAO,
                            TipoConteudoEntity tipoConteudo,
                            PaisEntity pais,
                            FeedbackEntity feedback)
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
