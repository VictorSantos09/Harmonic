using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
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
        TipoConteudoEntity tipoConteudo = new(dto.TipoConteudo.Nome) { Id = dto.TipoConteudo.Id };
        PaisEntity pais = new(dto.Pais.Nome) { Id = dto.Pais.Id };
        FeedbackEntity feedback = new(0, 0) { Id = dto.Feedback.Id };
        ConteudoEntity entity = new(dto.Titulo, dto.DataCadastro, dto.Descricao, tipoConteudo, pais, feedback);

        int result = await _adicionarConteudoRepository.AddAsync(entity, cancellationToken);
        if (result > 0) return Final.Success();
        return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo");
    }
}
