using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.TipoConteudo.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.TipoConteudo
{
    internal class TipoConteudoGetService : ITipoConteudoGetServices
    {
        private readonly ITipoConteudoGetRepository _tipoconteudoGetRepository;

        public TipoConteudoGetService(ITipoConteudoGetRepository tipoconteudoGetRepository)
        {
            _tipoconteudoGetRepository = tipoconteudoGetRepository;
        }

        public async Task<IFinal<IEnumerable<TipoConteudoEntity>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _tipoconteudoGetRepository.GetAllAsync(cancellationToken);
            return Final.Success(result);
        }

        public async Task<IFinal<TipoConteudoEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _tipoconteudoGetRepository.GetByIdAsync(id, cancellationToken);

            if (result.IsNull()) return Final.Failure(result, "conteudo.getById.NaoEncontrado", "Não foi encontrado nenhum conteúdo");

            return Final.Success(result);
        }
    }
}
