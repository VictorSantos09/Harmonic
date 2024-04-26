namespace Harmonic.Regras.Services.Conteudo.DTOs;

public record ConteudoDTO(int Id,
                          string Titulo,
                          DateTime DataCadastro,
                          string Descricao,
                          TipoConteudoDTO TipoConteudo,
                          PaisDTO Pais,
                          FeedbackDTO Feedback,
                          PlataformaDTO Plataforma)
{

}

public record FeedbackDTO(int Id, int TotalCurtidas, int TotalGosteis)
{

}

public record PaisDTO(string Nome, int Id)
{

}

public record TipoConteudoDTO(int Id, string Nome)
{
}

public record PlataformaDTO(string Nome, int Id, string URL) 
{ 
}
