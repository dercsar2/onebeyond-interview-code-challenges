using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using OneBeyondApi.Service;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OnLoanController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IReturnBookService _returnBookService;

        public OnLoanController(
            ILogger<BorrowerController> logger,
            IBorrowerRepository borrowerRepository,
            IReturnBookService returnBookService)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;
            _returnBookService = returnBookService;
        }

        [HttpGet]
        public IList<ActiveBorrower> GetActiveBorrowers()
        {
            return _borrowerRepository.GetActiveBorrowers();
        }

        [HttpPost]
        public void ReturnBookStock(Guid bookStockId)
        {
            _returnBookService.ReturnBook(bookStockId);
        }
    }
}