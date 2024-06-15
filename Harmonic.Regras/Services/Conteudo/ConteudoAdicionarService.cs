using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAdicionarService : IConteudoAdicionarService
{
    private readonly IConteudoAdicionarRepository _adicionarConteudoRepository;
    private readonly ITipoConteudoGetRepository _tipoConteudoGetRepository;
    private readonly IPaisGetRepository _paisGetRepository;
    private readonly IValidator<ConteudoEntity> _validator;
    private readonly IConteudoPlataformaAdicionarService _conteudoPlataformaAdicionarService;
    private readonly IPlataformaGetService _plataformaGetService;

    public ConteudoAdicionarService(IConteudoAdicionarRepository adicionarConteudoRepository,
                                    ITipoConteudoGetRepository tipoConteudoGetRepository,
                                    IPaisGetRepository paisGetRepository,
                                    IValidator<ConteudoEntity> validator,
                                    IPlataformaGetService plataformaGetService,
                                    IConteudoPlataformaAdicionarService conteudoPlataformaAdicionarService)
    {
        _adicionarConteudoRepository = adicionarConteudoRepository;
        _tipoConteudoGetRepository = tipoConteudoGetRepository;
        _paisGetRepository = paisGetRepository;
        _validator = validator;
        _conteudoPlataformaAdicionarService = conteudoPlataformaAdicionarService;
        _plataformaGetService = plataformaGetService;
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

            string[] urlArray = { "https://open.spotify.com/artist/3TVXtAsR1Inumwj472S9r4" };


            int idPlat;
            string contentUrl;

            foreach (var url in urlArray)
            {
                if (url.ToLower().Contains("youtube"))
                {
                    contentUrl = url.Substring(url.IndexOf(".com") + 4);
                    PlataformaEntity? Plat = await _plataformaGetService.GetByNameAsync("YOUTUBE", cancellationToken);

                    if (Plat == null) { Final.Failure(Plat, "plataforma.GetByNameAsync", "Não retornou nenhuma plataforma"); };

                    idPlat = Plat.Id;

                    var add = _conteudoPlataformaAdicionarService.AddAsync(new ConteudoPlataformaDTO(0, contentUrl, 30, idPlat), cancellationToken);

                }
                if (url.ToLower().Contains("spotify"))
                {
                    contentUrl = url.Substring(url.IndexOf("artist") + 6);

                    PlataformaEntity? Plat = await _plataformaGetService.GetByNameAsync("SPOTIFY", cancellationToken);

                    if (Plat == null) { Final.Failure(Plat, "plataforma.GetByNameAsync", "Não retornou nenhuma plataforma"); };

                    idPlat = Plat.Id;

                    var add = _conteudoPlataformaAdicionarService.AddAsync(new ConteudoPlataformaDTO(0, contentUrl, 30, idPlat), cancellationToken);

                }
                if (url.ToLower().Contains("deezer"))
                {
                    contentUrl = url.Substring(url.IndexOf("artist") + 6);

                    PlataformaEntity? Plat = await _plataformaGetService.GetByNameAsync("DEEZER", cancellationToken);

                    if (Plat == null) { Final.Failure(Plat, "plataforma.GetByNameAsync", "Não retornou nenhuma plataforma"); };

                    idPlat = Plat.Id;

                    var add = _conteudoPlataformaAdicionarService.AddAsync(new ConteudoPlataformaDTO(0, contentUrl, 30, idPlat), cancellationToken);
                }

            }

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
