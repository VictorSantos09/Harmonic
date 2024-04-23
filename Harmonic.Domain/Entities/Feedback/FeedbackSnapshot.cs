using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonic.Domain.Entities.Feedback
{
    public record FeedbackSnapshot(int ID, int TOTAL_CURTIDAS, int TOTAL_GOSTEI);
}
