﻿using FluentValidation;
using Harmonic.Domain.Entities.Base;
using Harmonic.Shared.Constants;

namespace Harmonic.Domain.Entities.Feedback;

internal class FeedbackValidator : Validator<FeedbackEntity, int>
{
    public FeedbackValidator(bool validateId = false) : base(validateId)
    {
        RuleFor(x => x.TotalGosteis).NotNull()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .GreaterThanOrEqualTo(0)
            .WithMessage(CONSTANTS.INT.MESSAGE_MAIOR_QUE_ZERO);

        RuleFor(x => x.TotalCurtidas).NotNull()
            .WithMessage(CONSTANTS.STRING.MESSAGE_VAZIO)
            .GreaterThanOrEqualTo(0)
            .WithMessage(CONSTANTS.INT.MESSAGE_MAIOR_QUE_ZERO);
    }
}
