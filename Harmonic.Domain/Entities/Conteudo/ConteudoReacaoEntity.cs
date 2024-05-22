using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Conteudo;
public class ConteudoReacaoEntity : IEntity<string>
{
    public string Id { get; set; }
    public int IdConteudo { get; set; }
    public string IdUsuario { get; set; }
    public bool Curtiu { get; set; }
}
