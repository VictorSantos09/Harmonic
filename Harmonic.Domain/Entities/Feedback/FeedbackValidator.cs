using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.Feedback;

internal class FeedbackValidator : Validator<FeedbackEntity, int>
{
    public FeedbackValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.TotalGosteis).NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .GreaterThan(0)
            .WithMessage(CONSTANTS.INT.MESSAGE_MAIOR_QUE_ZERO);

        RuleFor(x => x.TotalCurtidas).NotEmpty()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .GreaterThan(0)
            .WithMessage(CONSTANTS.INT.MESSAGE_MAIOR_QUE_ZERO);
    }
}
