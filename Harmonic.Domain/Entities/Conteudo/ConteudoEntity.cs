using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Conteudo;

public class ConteudoEntity : IEntity<ConteudoEntity, ConteudoSnapshot, int>
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Descricao { get; set; }
    public TipoConteudoEntity TipoConteudo { get; set; }
    public PaisEntity Pais { get; set; }
    public FeedbackEntity Feedback { get; set; }

    public ConteudoEntity(string titulo,
                                DateTime dataCadastro,
                                  string descricao,
                                  TipoConteudoEntity tipoConteudo,
                                  PaisEntity pais,
                                  FeedbackEntity feedback)
    {
        Titulo = titulo;
        DataCadastro = dataCadastro;
        Descricao = descricao;
        TipoConteudo = tipoConteudo;
        Pais = pais;
        Feedback = feedback;
    }

    public ConteudoEntity()
    {

    }

    public static ConteudoEntity? FromSnapshot(ConteudoSnapshot? snapshot)
    {
        if (snapshot is null) return null;

        return new(snapshot.TITULO,
                   snapshot.DATA_CADASTRO,
                   snapshot.DESCRICAO,
                   snapshot.TipoConteudo,
                   snapshot.Pais,
                   snapshot.Feedback);
    }

    public ConteudoSnapshot ToSnapshot()
    {
        return new ConteudoSnapshot(Id, Titulo, DateTime.Parse(DataCadastro.ToString()), Descricao, TipoConteudo, Pais, Feedback);
    }
}
