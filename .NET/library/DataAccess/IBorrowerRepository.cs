using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public List<Borrower> GetBorrowers();

        public List<ActiveBorrower> GetActiveBorrowers();

        public Guid AddBorrower(Borrower borrower);
    }
}
