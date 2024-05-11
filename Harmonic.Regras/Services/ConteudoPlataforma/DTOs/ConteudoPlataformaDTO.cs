using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Plataforma;

namespace Harmonic.Regras.Services.ConteudoPlataforma.DTOs;

public record ConteudoPlataformaDTO(int Id, string URL, int IdConteudo, int IdPlataforma)
{

}
