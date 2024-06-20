namespace Harmonic.Regras.Services.Conteudo.DTOs;

public class ConteudoDTO
{
    public int Id {get; set;}
    public string Titulo {get; set;}
    public string Descricao {get; set;}
    public int IdTipoConteudo {get; set;}
    public int IdPais {get; set;}
    public int IdPlataforma { get; set; }
    public IEnumerable<string> Urls { get; set; }
    public string Imagem { get; set; }

    public ConteudoDTO(int id,
                       string titulo,
                       string descricao,
                       int idTipoConteudo,
                       int idPais,
                       int idPlataforma,
                       IEnumerable<string> urls,
                       string imagem)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        IdTipoConteudo = idTipoConteudo;
        IdPais = idPais;
        IdPlataforma = idPlataforma;
        Urls = urls;
        Imagem = imagem;
    }
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
