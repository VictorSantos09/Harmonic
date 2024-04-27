using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Contracts.TipoConteudo;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.TipoConteudo.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.TipoConteudo
{
    internal class TipoConteudoAdicionarService : ITipoConteudoAdicionarServices
    {
        private readonly ITipoConteudoAdicionarRepository _adicionarRepository;

        public TipoConteudoAdicionarService(ITipoConteudoAdicionarRepository adicionarRepository)
        {
            _adicionarRepository = adicionarRepository;
        }

        public async Task<IFinal> AddAsync(TipoConteudoDTO dto, CancellationToken cancellationToken)
        {

            TipoConteudoEntity entity = new(dto.Nome);

            int result = await _adicionarRepository.AddAsync(entity, cancellationToken);
            if (result > 0) return Final.Success();
            return Final.Failure("TipoConteudo.Add.Falha", "Não foi possível adicionar o tipo de conteúdo");
        }

    }
}
