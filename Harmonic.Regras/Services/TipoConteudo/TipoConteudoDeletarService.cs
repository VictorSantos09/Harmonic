using Harmonic.Infra.Enums;
using Harmonic.Infra.Repositories.Common;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.TipoConteudo.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.TipoConteudo
{
    internal class TipoConteudoDeletarService : ITipoConteudoDeletarServices
    {
        private readonly ITipoConteudoDeletarRepository _tipoconteudoDeletarRepository;
        private readonly IExistsRepository _existsRepository;

        public TipoConteudoDeletarService(ITipoConteudoDeletarRepository tipoconteudoDeletarRepository, IExistsRepository existsRepository)
        {
            _tipoconteudoDeletarRepository = tipoconteudoDeletarRepository;
            _existsRepository = existsRepository;
        }

        public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var exists = await _existsRepository.ExistsAsync(TABLES.TIPOS_CONTEUDOS, id, cancellationToken);

            if (!exists) return Final.Failure("conteudo.delete.NaoEncontrado", "O conteúdo não foi encontrado");

            var result = await _tipoconteudoDeletarRepository.DeleteAsync(id, cancellationToken);

            if (result > 0) return Final.Success("conteúdo deletado com sucesso");

            return Final.Failure("conteudo.delete.Falha", "não foi possível deletar o conteúdo");
        }
    }
}
