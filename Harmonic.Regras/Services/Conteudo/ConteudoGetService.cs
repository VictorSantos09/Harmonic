using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoGetService : IConteudoGetService
{
    private readonly IConteudoGetRepository _conteudoGetRepository;

    private readonly ITipoConteudoGetRepository _tipoConteudoRepository;
    private readonly IFeedbackGetRepository _feedbackRepository;
    private readonly IPaisGetRepository _paisRepository;


    public ConteudoGetService(IConteudoGetRepository conteudoGetRepository, ITipoConteudoGetRepository tipoConteudoRepository, IFeedbackGetRepository feedbackRepository, IPaisGetRepository paisRepository)
    {
        _conteudoGetRepository = conteudoGetRepository;
        _tipoConteudoRepository = tipoConteudoRepository;
        _feedbackRepository = feedbackRepository;
        _paisRepository = paisRepository;
            }

    public async Task<IFinal<IEnumerable<ConteudoEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetAllAsync(cancellationToken);

        return Final.Success(result);
    }

    public async Task<IFinal<ConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _conteudoGetRepository.GetByIdAsync(id, cancellationToken);

        if (result.IsNull()) return Final.Failure(result, "conteudo.getById.NaoEncontrado", "Não foi encontrado nenhum conteúdo");

        result.TipoConteudo =await _tipoConteudoRepository.GetByIdAsync(result.ID_TIPO_CONTEUDO, cancellationToken);
        result.Feedback = await _feedbackRepository.GetByIdAsync(result.ID_FEEDBACK, cancellationToken);
        result.Pais = await _paisRepository.GetByIdAsync(result.ID_PAIS, cancellationToken);

        return Final.Success(result);
    }
}
