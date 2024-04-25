using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Feedback;

public class FeedbackEntity : IEntity<FeedbackEntity, FeedbackSnapshot, int>
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

    public static FeedbackEntity? FromSnapshot(FeedbackSnapshot? snapshot)
    {
        if (snapshot is null) return null;

        return new(snapshot.TOTAL_CURTIDAS,
                   snapshot.TOTAL_GOSTEIS)
                   { Id = snapshot.ID};
    }

    public FeedbackSnapshot ToSnapshot()
    {
        return new(Id, TotalCurtidas, TotalGosteis);
    }

}
