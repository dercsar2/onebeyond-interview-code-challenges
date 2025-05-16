namespace OneBeyondApi.Model;

//public record Fine(Borrower Borrower, BookStock BookStock, decimal FineAmount, DateTime? Paid, Guid Id = default);

public class Fine
{
    public Guid Id { get; set; }

    public required Borrower Borrower { get; set; }

    public required BookStock BookStock { get; set; }

    public decimal FineAmount { get; set; }

    public DateTime? Paid { get; set; }
}