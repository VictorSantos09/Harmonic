namespace Harmonic.Regras.Services.Conteudo.DTOs;

public record ConteudoDTO(int Id,
                          string Titulo,
                          DateOnly DataCadastro,
                          string Descricao,
                          TipoConteudoDTO TipoConteudo,
                          PaisDTO Pais,
                          FeedbackDTO Feedback)
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
