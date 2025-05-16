using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public interface IFineRepository
{
    Fine Save(Fine fine);

    List<Fine> GetAll();
}