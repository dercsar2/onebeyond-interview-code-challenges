using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface ICatalogueRepository
    {
        public List<BookStock> GetCatalogue();

        public BookStock? GetBookStockById(Guid bookStockId);

        public BookStock ReturnBookByBookStockId(Guid bookStockId);

        public List<BookStock> SearchCatalogue(CatalogueSearch search);

        public AvailabilityResult QueryAvailability(Book title);
    }
}
