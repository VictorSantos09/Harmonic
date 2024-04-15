using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Feedback;

public class FeedbackEntity : IEntity<int>
{
    public int Id { get; set; }
    public int TotalCurtidas { get; set; }
    public int TotalGosteis { get; set; }

    public FeedbackEntity(int totalCurtidas, int totalGosteis)
    {
        TotalCurtidas = totalCurtidas;
        TotalGosteis = totalGosteis;
    }

    public FeedbackEntity()
    {

    }
}
