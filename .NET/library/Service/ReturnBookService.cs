using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Service;

public interface IReturnBookService
{
    public BookStock ReturnBook(Guid bookStockId);
}

public class ReturnBookService : IReturnBookService
{
    private readonly ICatalogueRepository catalogueRepository;

    public ReturnBookService(ICatalogueRepository catalogueRepository)
    {
        this.catalogueRepository = catalogueRepository;
    }

    public BookStock ReturnBook(Guid bookStockId)
    {
        var book = catalogueRepository.GetBookStockById(bookStockId);
        if (book == null)
            throw new InvalidOperationException($"Book item not found: ${bookStockId}");
        if (book.OnLoanTo == null)
            throw new InvalidOperationException($"Return book not on loan: ${bookStockId}");
        return catalogueRepository.ReturnBookByBookStockId(bookStockId);
    }
}