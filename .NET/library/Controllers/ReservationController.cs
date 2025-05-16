using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ReservationController : ControllerBase
{
    private readonly IBookRepository bookRepository;
    private readonly ICatalogueRepository catalogueRepository;

    public ReservationController(IBookRepository bookRepository, ICatalogueRepository catalogueRepository)
    {
        this.bookRepository = bookRepository;
        this.catalogueRepository = catalogueRepository;
    }

    [HttpGet]
    public AvailabilityResult QueryAvailability(Guid bookId)
    {
        var title = bookRepository.GetBooks().First(b => b.Id == bookId);
        return catalogueRepository.QueryAvailability(title);
    }
}