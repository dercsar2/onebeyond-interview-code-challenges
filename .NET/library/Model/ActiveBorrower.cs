namespace OneBeyondApi.Model
{
    public class ActiveBorrower
    {
        public ActiveBorrower(Borrower borrower, IEnumerable<BookStock> bookItems)
        {
            Borrower = borrower;
            BooksOnLoan = [.. bookItems];
        }

        public Borrower Borrower { get; init; }

        public List<BookStock> BooksOnLoan { get; init; }
    }
}
