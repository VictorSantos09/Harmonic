﻿using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Common;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Pais.Contracts;
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
    private readonly IQueryRepository _queryRepository;
    public const string DEFAULT_IMAGE = "https://img.freepik.com/vetores-gratis/design-plano-desenhado-a-mao-sem-sinal-de-foto_23-2149278076.jpg?w=826&t=st=1718560107~exp=1718560707~hmac=3a88bf61784f0515a9173c3a091ee742d81f52eb9b94987a61e9f2dcaa2c25ba";

    public ConteudoAdicionarService(IConteudoAdicionarRepository adicionarConteudoRepository,
                                    ITipoConteudoGetRepository tipoConteudoGetRepository,
                                    IPaisGetRepository paisGetRepository,
                                    IValidator<ConteudoEntity> validator,
                                    IPlataformaGetService plataformaGetService,
                                    IConteudoPlataformaAdicionarService conteudoPlataformaAdicionarService,
                                    IQueryRepository queryRepository)
    {
        _adicionarConteudoRepository = adicionarConteudoRepository;
        _tipoConteudoGetRepository = tipoConteudoGetRepository;
        _paisGetRepository = paisGetRepository;
        _validator = validator;
        _conteudoPlataformaAdicionarService = conteudoPlataformaAdicionarService;
        _plataformaGetService = plataformaGetService;
        _queryRepository = queryRepository;
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

            dto.Imagem ??= DEFAULT_IMAGE;
            ConteudoEntity entity = new(dto.Titulo, DateTime.Now, dto.Descricao, tipoConteudo, pais, feedback, dto.Imagem);

            var validationResult = await _validator.ValidateAsync(entity, cancellationToken);
            if (!validationResult.IsValid) return Final.Failure("Conteudo.Add.Invalido", "Dados do conteúdo são inválidos");

            int resultConteudo = await _adicionarConteudoRepository.AddAsync(entity, cancellationToken);

            if (resultConteudo > 0)
            {
                IFinal resultConteudoPlataforma;
                foreach (var url in dto.Urls)
                {
                    resultConteudoPlataforma = await CadastrarPlataformasAsync(url, cancellationToken);

                    if (resultConteudoPlataforma.IsFailure)
                    {
                        _adicionarConteudoRepository.Rollback();
                        return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo na plataforma");
                    }
                }

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

    private async Task<IFinal> CadastrarPlataformasAsync(string url, CancellationToken cancellationToken)
    {
        string contentUrl;

        if (url.Contains("youtube"))
        {
            contentUrl = GetConteudoIdUrl(url);
            return await GravarConteudoPlataformaAsync(contentUrl, "YOUTUBE", cancellationToken);
        }
        else if (url.Contains("spotify"))
        {
            contentUrl = GetConteudoIdUrl(url);
            return await GravarConteudoPlataformaAsync(contentUrl, "SPOTIFY", cancellationToken);

        }
        else if (url.Contains("deezer"))
        {
            contentUrl = GetConteudoIdUrl(url);
            return await GravarConteudoPlataformaAsync(contentUrl, "DEEZER", cancellationToken);
        }
        else
        {
            return Final.Failure("conteudo.add.PlataformaNaoEncontrada", "Não foi encontrada a plataforma");
        }
    }

    private async Task<IFinal<int>> GravarConteudoPlataformaAsync(string conteudoUrl, string nomePlataforma, CancellationToken cancellationToken)
    {
        var plataforma = await _plataformaGetService.GetByNameAsync(nomePlataforma, cancellationToken);

        if (plataforma is null)
            return Final.Failure(0, "conteudo.add.PlataformaNaoEncontrada", "Não foi encontrada a plataforma");

        var idConteudo = await _queryRepository.QuerySingleOrDefaultAsync<int>("SELECT MAX(ID) FROM CONTEUDOS");

        var result = await _conteudoPlataformaAdicionarService.AddAsync(new ConteudoPlataformaDTO(0, conteudoUrl, idConteudo, plataforma.Id), cancellationToken);

        if (result.IsSuccess)
            return Final.Success(plataforma.Id);

        return Final.Failure(0, "conteudo.add.ConteudoPlataformaNaoCadastrado", "Não foi cadastrado o conteúdo plataforma");
    }

    private static string GetConteudoIdUrl(string url, string prefix = "com/")
    {
        int startIndex = url.IndexOf(prefix) + prefix.Length;
        return url[startIndex..];
    }
}
