namespace OneBeyondApi.Model
{
    public class ActiveBorrower
    {
        public ActiveBorrower(Borrower borrower, IEnumerable<string> titles)
        {
            Borrower = borrower;
            BooksOnLoan = [.. titles];
        }

        public Borrower Borrower { get; init; }

        public List<string> BooksOnLoan { get; init; }
    }
}
