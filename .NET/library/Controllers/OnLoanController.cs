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

        public OnLoanController(ILogger<BorrowerController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;
        }

        [HttpGet]
        public IList<ActiveBorrower> GetActiveBorrowers()
        {
            return _borrowerRepository.GetActiveBorrowers();
        }
    }
}