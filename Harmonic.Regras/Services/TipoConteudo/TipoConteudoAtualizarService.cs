using FluentValidation;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.TipoConteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.TipoConteudo.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.TipoConteudo
{
    internal class TipoConteudoAtualizarService : ITipoConteudoAtualizarServices
    {
        private readonly ITipoConteudoAtualizarRepository _tipoconteudoAtualizarRepository;
        private readonly IValidator<TipoConteudoEntity> _validator;

        public TipoConteudoAtualizarService(ITipoConteudoAtualizarRepository tipoconteudoAtualizarRepository,
                                        IValidator<TipoConteudoEntity> validator)
        {
            _tipoconteudoAtualizarRepository = tipoconteudoAtualizarRepository;
            _validator = validator;
        }

        public async Task<IFinal> UpdateAsync(TipoConteudoDTO dto, CancellationToken cancellationToken)
        {

            TipoConteudoEntity tipoconteudo = new(dto.Nome) { Id = dto.Id };

            var validationResult = await _validator.ValidateAsync(tipoconteudo, cancellationToken);

            if (!validationResult.IsValid) return Final.Failure("conteudo.atualizar.Invalido", "dados do conteúdo são inválidos");

            var result = await _tipoconteudoAtualizarRepository.UpdateAsync(tipoconteudo, cancellationToken);

            if (result > 0) return Final.Success("tipo conteúdo atualizado com sucesso");

            return Final.Failure("conteudo.atualizar.Falha", "não foi possível atualizar o tipo conteúdo");
        }
    }
}