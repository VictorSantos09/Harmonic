namespace Harmonic.Domain.Entities.Feedback;

public class FeedbackSnapshot
{
    public int ID{ get; set; }
    public int TOTAL_CURTIDAS{ get; set; }
    public int TOTAL_GOSTEIS{ get; set; }

    public FeedbackSnapshot(int iD, int tOTAL_CURTIDAS, int tOTAL_GOSTEIS)
    {
        ID = iD;
        TOTAL_CURTIDAS = tOTAL_CURTIDAS;
        TOTAL_GOSTEIS = tOTAL_GOSTEIS;
    }

    public FeedbackSnapshot()
    {
        
    }
};

