using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ReservationController : ControllerBase
{
    private readonly IBookRepository bookRepository;
    private readonly IBorrowerRepository borrowerRepository;
    private readonly ICatalogueRepository catalogueRepository;

    public ReservationController(IBookRepository bookRepository, IBorrowerRepository borrowerRepository, ICatalogueRepository catalogueRepository)
    {
        this.bookRepository = bookRepository;
        this.borrowerRepository = borrowerRepository;
        this.catalogueRepository = catalogueRepository;
    }

    [HttpGet]
    public AvailabilityResult QueryAvailability(Guid bookId)
    {
        var title = bookRepository.GetBooks().First(b => b.Id == bookId);
        return catalogueRepository.QueryAvailability(title);
    }

    [HttpPost]
    public void CreateReservation(Guid bookStockId, Guid borrowerId, DateTime startDate, int daysLong)
    {
        var book = catalogueRepository.GetBookStockById(bookStockId);
        if (book == null)
            throw new InvalidOperationException($"Book item not found: ${bookStockId}");
        var borrower = borrowerRepository.GetBorrowers().First(b => b.Id == borrowerId);
        catalogueRepository.CreateReservation(book, borrower, startDate, daysLong);
    }
}