namespace Harmonic.Regras.Services.Conteudo.DTOs;

public record ConteudoDTO(int Id,
                          string Titulo,
                          string Descricao,
                          int IdTipoConteudo,
                          int IdPais,
                          int IdPlataforma)
{

}

public record FeedbackDTO(int Id, int TotalCurtidas, int TotalGosteis)
{

}

public record PaisDTO(string Nome, int Id, string Icon)
{

}

public record TipoConteudoDTO(int Id, string Nome)
{
}

public record PlataformaDTO(string Nome, int Id, string URL) 
{ 
}
