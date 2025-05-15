using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        public BorrowerRepository()
        {
        }
        public List<Borrower> GetBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .ToList();
                return list;
            }
        }

        public List<ActiveBorrower> GetActiveBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .Join(context.Catalogue
                        .Include(c => c.Book)
                        .Where(x => x.OnLoanTo != null),
                        b => b.Id,
                        c => c.OnLoanTo!.Id,
                        (borrower, bookStock) => new { borrower, bookStock })
                    .GroupBy(item => item.borrower)
                    .Select(g => new ActiveBorrower(g.Key, g.Select(gi => gi.bookStock.Book.Name)))
                    .ToList();
                return list;
            }
        }

        public Guid AddBorrower(Borrower borrower)
        {
            using (var context = new LibraryContext())
            {
                context.Borrowers.Add(borrower);
                context.SaveChanges();
                return borrower.Id;
            }
        }
    }
}
