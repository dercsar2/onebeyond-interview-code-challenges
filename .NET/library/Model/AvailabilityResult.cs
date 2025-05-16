namespace OneBeyondApi.Model;

public class AvailabilityResult
{
    public List<BookStock> ReadyItems { get; set; }

    public List<Availability> ReservableItems { get; set; }
}