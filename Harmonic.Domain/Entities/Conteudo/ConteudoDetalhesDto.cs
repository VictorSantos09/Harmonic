namespace Harmonic.Domain.Entities.Conteudo;
public record ConteudoDetalhesDto
{
    public int id { get; set; }
    public string titulo { get; set; }
    public string descricao { get; set; }
    public string tipo { get; set; }
    public string pais { get; set; }
    public string? icon { get; set; }
    public int total_curtidas { get; set; }
    public int total_gosteis { get; set; }
    public string imagem { get; set; }
}
