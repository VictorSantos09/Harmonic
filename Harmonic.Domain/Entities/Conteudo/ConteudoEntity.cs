using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Shared.Exceptions;
using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Conteudo;

public class ConteudoEntity : IEntity<int>
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
}
