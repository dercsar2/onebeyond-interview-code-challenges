using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Service;

public interface IFineService
{
    Fine? CreateFine(BookStock bookStock);
}

public class FineService : IFineService
{
    private const decimal DAILY_FINE = 120;

    private readonly IFineRepository fineRepository;

    public FineService(IFineRepository fineRepository)
    {
        this.fineRepository = fineRepository;
    }

    public Fine? CreateFine(BookStock bookStock)
    {
        if (bookStock.LoanEndDate == null || bookStock.LoanEndDate >= DateTime.Now)
            return null;

        var fine = fineRepository.Save(new Fine()
            {
                Borrower = bookStock.OnLoanTo!,
                BookStock = bookStock,
                FineAmount = CalculateFineAmount(bookStock.LoanEndDate.Value, DateTime.Now),
                Paid = null
            });

        return fine;
    }

    private decimal CalculateFineAmount(DateTime loadEndDate, DateTime returnDate) =>
        returnDate.Subtract(loadEndDate).Days * DAILY_FINE;
}