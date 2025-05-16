using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FineController : ControllerBase
{
    private readonly IFineRepository fineRepository;

    public FineController(IFineRepository fineRepository)
    {
        this.fineRepository = fineRepository;
    }

    [HttpGet]
    public IList<Fine> GetAll()
    {
        return fineRepository.GetAll();
    }
}