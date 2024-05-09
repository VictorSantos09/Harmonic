using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Shared.Exceptions;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Conteudo;

public class ConteudoEntity : IEntity<ConteudoEntity, ConteudoSnapshot, int>
{

    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Descricao { get; set; }
    public int ID_FEEDBACK { get; set; }
    public int ID_PAIS { get; set; }
    public int ID_TIPO_CONTEUDO { get; set; }

    public TipoConteudoEntity TipoConteudo { get; set; }
    public PaisEntity Pais { get; set; }
    public FeedbackEntity Feedback { get; set; }

    public ConteudoEntity(int iD,
        string titulo,
                                DateTime dataCadastro,
                                  string descricao,
                                  int id_FEEDBACK,
                                  int id_PAIS,
                                  int id_TIPO_CONTEUDO,
                                  TipoConteudoEntity tipoConteudo,
                                  PaisEntity pais,
                                  FeedbackEntity feedback)
    {
        Id = iD;
        Titulo = titulo;
        DataCadastro = dataCadastro;
        Descricao = descricao;
        ID_FEEDBACK = id_FEEDBACK;
        ID_PAIS = id_PAIS;
        ID_TIPO_CONTEUDO = id_TIPO_CONTEUDO;
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




        return new(snapshot.ID,
            snapshot.TITULO,
                   snapshot.DATA_CADASTRO,
                   snapshot.DESCRICAO,
                   snapshot.ID_FEEDBACK,
                   snapshot.ID_PAIS,
                   snapshot.ID_TIPO_CONTEUDO,
                   TipoConteudoEntity.FromSnapshot(snapshot.TipoConteudo) ?? throw new SnapshotNullException<TipoConteudoSnapshot>(),
                   PaisEntity.FromSnapshot(snapshot.Pais) ?? throw new SnapshotNullException<PaisSnapshot>(),
                   FeedbackEntity.FromSnapshot(snapshot.Feedback) ?? throw new SnapshotNullException<FeedbackEntity>()); 
        ;
    }

    public ConteudoSnapshot ToSnapshot()
    {
        return new ConteudoSnapshot(Id, Titulo, DateTime.Parse(DataCadastro.ToString()), Descricao, ID_FEEDBACK, ID_PAIS, ID_TIPO_CONTEUDO, TipoConteudo.ToSnapshot(), Pais.ToSnapshot(), Feedback.ToSnapshot());
    }
}
