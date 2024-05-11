using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAdicionarService : IConteudoAdicionarService
{
    private readonly IConteudoAdicionarRepository _adicionarConteudoRepository;
    private readonly ITipoConteudoGetRepository _tipoConteudoGetRepository;
    private readonly IPaisGetRepository _paisGetRepository;
    private readonly IValidator<ConteudoEntity> _validator;

    public ConteudoAdicionarService(IConteudoAdicionarRepository adicionarConteudoRepository,
                                    ITipoConteudoGetRepository tipoConteudoGetRepository,
                                    IPaisGetRepository paisGetRepository,
                                    IValidator<ConteudoEntity> validator)
    {
        _adicionarConteudoRepository = adicionarConteudoRepository;
        _tipoConteudoGetRepository = tipoConteudoGetRepository;
        _paisGetRepository = paisGetRepository;
        _validator = validator;
    }

    public async Task<IFinal> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        _adicionarConteudoRepository.BeginTransaction();
        try
        {
            var tipoConteudo = await _tipoConteudoGetRepository.GetByIdAsync(dto.IdTipoConteudo, cancellationToken);
            var pais = await _paisGetRepository.GetByIdAsync(dto.IdPais, cancellationToken);

            if (tipoConteudo is null) return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo, tipo conteudo não encontrado");
            if (pais is null) return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo, pais não encontrado");

            FeedbackEntity feedback = new(0, 0);
            ConteudoEntity entity = new(dto.Titulo, DateTime.Now, dto.Descricao, tipoConteudo, pais, feedback);

            var validationResult = await _validator.ValidateAsync(entity, cancellationToken);
            if (!validationResult.IsValid) return Final.Failure("Conteudo.Add.Invalido", "Dados do conteúdo são inválidos");

            int result = await _adicionarConteudoRepository.AddAsync(entity, cancellationToken);

            if (result > 0)
            {
                _adicionarConteudoRepository.Commit();
                return Final.Success();
            }

            _adicionarConteudoRepository.Rollback();
            return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo");
        }
        catch (Exception)
        {
            _adicionarConteudoRepository.Rollback();
            throw;
        }
    }
}
