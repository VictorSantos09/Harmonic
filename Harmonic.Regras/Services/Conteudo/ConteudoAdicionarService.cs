using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Contracts.Conteudo;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAdicionarService : IConteudoAdicionarService
{
    private readonly IConteudoAdicionarRepository _adicionarConteudoRepository;

    public ConteudoAdicionarService(IConteudoAdicionarRepository adicionarConteudoRepository)
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
