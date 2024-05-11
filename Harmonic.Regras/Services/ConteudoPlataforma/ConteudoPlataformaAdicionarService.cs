using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoPlataforma;

internal class ConteudoPlataformaAdicionarService : IConteudoPlataformaAdicionarService
{
    private readonly IConteudoPlataformaAdicionarRepository _adicionarRepository;
    private readonly IConteudoGetService _conteudoGetService;
    private readonly IPlataformaGetService _plataformaGetService;

    public ConteudoPlataformaAdicionarService(IConteudoPlataformaAdicionarRepository adicionarRepository,
                                              IConteudoGetService conteudoGetService,
                                              IPlataformaGetService plataformaGetService)
    {
        _adicionarRepository = adicionarRepository;
        _conteudoGetService = conteudoGetService;
        _plataformaGetService = plataformaGetService;
    }

    public async Task<IFinal> AddAsync(ConteudoPlataformaDTO dto, CancellationToken cancellationToken)
    {
        var conteudoResult = await _conteudoGetService.GetByIdAsync(dto.IdConteudo, cancellationToken);
        var plataformaResult = await _plataformaGetService.GetByIdAsync(dto.IdPlataforma, cancellationToken);

        if (conteudoResult.IsFailure) return conteudoResult;
        if (plataformaResult.IsFailure) return plataformaResult;

        ConteudoPlataformaEntity conteudoPlataforma = new(dto.URL, conteudoResult.Data!, plataformaResult.Data!);

        int result = await _adicionarRepository.AddAsync(conteudoPlataforma, cancellationToken);

        if (result > 0) return Final.Success();
        
        return Final.Failure("ConteudoPlataforma.Add.Falha", "Falha ao inserir registro");
    }
}
