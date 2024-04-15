using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo.Add;

internal class AdicionarConteudoService : IAdicionarConteudoService
{
    private readonly IAdicionarConteudoRepository _adicionarConteudoRepository;

    public AdicionarConteudoService(IAdicionarConteudoRepository adicionarConteudoRepository)
    {
        _adicionarConteudoRepository = adicionarConteudoRepository;
    }

    public async Task<IFinal> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        TipoConteudoEntity tipoConteudo = new(dto.TipoConteudo.Nome);
        PaisEntity pais = new(dto.Pais.Nome);
        FeedbackEntity feedback = new(0, 0);
        ConteudoEntity entity = new(dto.Titulo, dto.DataCadastro, dto.Descricao, tipoConteudo, pais, feedback);

        int result = await _adicionarConteudoRepository.AddAsync(entity, cancellationToken);
        if (result > 0) return Final.Success();
        return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo");
    }
}
