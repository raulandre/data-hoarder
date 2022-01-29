namespace Hoarder.Configuration;

public class Interval
{
    public int Duration { get; set; }
    public Unit Unit { get; set; }

    public TimeSpan AsTimeSpan()
    {

        TimeSpan ts;

        switch(Unit)
        {
            case Unit.Seconds:
                ts = new TimeSpan(0, 0, Duration);
                break;
            case Unit.Minutes:
                ts = new TimeSpan(0, Duration, 0);
                break;
            case Unit.Hours:
                ts = new TimeSpan(Duration, 0, 0);
                break;
            default:
                throw new Exception("Invalid Duration");
        }

        return ts;
    }
}