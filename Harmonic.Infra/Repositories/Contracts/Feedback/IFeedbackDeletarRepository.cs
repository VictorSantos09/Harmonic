﻿using Harmonic.Domain.Entities.Feedback;
using QuickKit.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonic.Infra.Repositories.Contracts.Feedback;

public interface IFeedbackDeletarRepository : IDeleteRepository <FeedbackEntity, int>
{
}
