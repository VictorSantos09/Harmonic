using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;

namespace Harmonic.Regras.Services.ConteudoPlataforma.DTOs;

public record ConteudoPlataformaDTO(int ID, string URL, ConteudoEntity Conteudo, PlataformaEntity Plataforma)
{

}
