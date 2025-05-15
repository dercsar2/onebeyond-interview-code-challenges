using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OnLoanController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly ICatalogueRepository _catalogueRepository;

        public OnLoanController(
            ILogger<BorrowerController> logger,
            IBorrowerRepository borrowerRepository,
            ICatalogueRepository catalogueRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;
            _catalogueRepository = catalogueRepository;
        }

        [HttpGet]
        public IList<ActiveBorrower> GetActiveBorrowers()
        {
            return _borrowerRepository.GetActiveBorrowers();
        }

        [HttpPost]
        public void ReturnBookStock(Guid bookStockId)
        {
            var bookStock = _catalogueRepository.GetBookStockById(bookStockId);
            if (bookStock == null)
                throw new InvalidOperationException();
            if (bookStock.OnLoanTo == null)
                throw new InvalidOperationException();
            _catalogueRepository.ReturnBookByBookStockId(bookStockId);
        }
    }
}