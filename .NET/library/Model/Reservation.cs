namespace OneBeyondApi.Model;

public class Reservation
{
    public Guid Id { get; set; }

    public BookStock BookStock { get; set; }

    public DateTime LoanStartDate { get; set; }

    public DateTime LoanEndDate { get; set; }
    
    public Borrower ReservingBorrower { get; set; }
}