using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public class FineRepository : IFineRepository
{
    public Fine Save(Fine fine)
    {
        using var c = new LibraryContext();
        c.Fines.Add(fine);
        c.SaveChanges();
        return fine;
    }

    public List<Fine> GetAll()
    {
        using var c = new LibraryContext();
        return c.Fines.ToList();
    }
}