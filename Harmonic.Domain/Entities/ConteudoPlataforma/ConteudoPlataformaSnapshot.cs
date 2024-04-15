using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;

namespace Harmonic.Domain.Entities.ConteudoPlataforma;

public record ConteudoPlataformaSnapshot(int ID, string URL, ConteudoSnapshot ConteudoSnapshot, PlataformaSnapshot PlataformaSnapshot)
{

}