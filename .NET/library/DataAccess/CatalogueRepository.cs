using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }
        public List<BookStock> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .ToList();
                return list;
            }
        }

        public BookStock? GetBookStockById(Guid bookStockId)
        {
            using (var c = new LibraryContext())
            {
                return c.Catalogue
                    .Include(c => c.Book)
                    .Include(c => c.OnLoanTo)
                    .FirstOrDefault(x => x.Id == bookStockId);
            }
        }

        public BookStock ReturnBookByBookStockId(Guid bookStockId)
        {
            using (var c = new LibraryContext())
            {
                var bookStock = c.Catalogue
                    .Include(c => c.Book)
                    .Include(c => c.OnLoanTo)
                    .First(x => x.Id == bookStockId);
                bookStock.LoanEndDate = null;
                bookStock.OnLoanTo = null;
                c.SaveChanges();
                return bookStock;
            }
        }


        public List<BookStock> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author))
                    {
                        list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName))
                    {
                        list = list.Where(x => x.Book.Name.Contains(search.BookName));
                    }
                }

                return list.ToList();
            }
        }

        public AvailabilityResult QueryAvailability(Book title)
        {
            using var c = new LibraryContext();
            return QueryAvailability(title, c);
        }

        private AvailabilityResult QueryAvailability(Book title, LibraryContext c)
        {
            var availableBooks = c.Catalogue.Where(x => x.Book.Id == title.Id && x.OnLoanTo == null).ToList();
            if (availableBooks.Any())
            {
                return new AvailabilityResult() { ReadyItems = availableBooks, ReservableItems = [] };
            }

            var data = c.Catalogue
                .Include(bs => bs.Book)
                .Where(x => x.Book.Id == title.Id && x.OnLoanTo != null)
                .Join(c.Reservations, bookStock => bookStock.Id, reservation => reservation.BookStock.Id, (bookStock, reservation) => reservation)
                .GroupBy(reservation => reservation.BookStock)
                .Select(grouping => new Availability() { BookStock = grouping.Key, ReservableFrom = grouping.Max(x => x.LoanEndDate) })
                .ToList();
            return new AvailabilityResult() { ReadyItems = [], ReservableItems = data };
        }

        public void CreateReservation(BookStock bookStock, Borrower borrower, DateTime startDate, int daysLong)
        {
            using var c = new LibraryContext();
            var availability = QueryAvailability(bookStock.Book, c);
            if (availability.ReadyItems.Any())
                throw new InvalidOperationException("Reserve an available book is not allowed. Implement loan functionality.");
            var reservabkleBook = availability.ReservableItems.Single(x => x.BookStock.Id == bookStock.Id);
            if (startDate < reservabkleBook.ReservableFrom)
                throw new InvalidOperationException("Cannot reserve for a date when the book is reserved.");

            var res = new Reservation() { BookStock = bookStock, ReservingBorrower = borrower, LoanStartDate = startDate, LoanEndDate = startDate.AddDays(daysLong) };
            c.Reservations.Add(res);
            c.SaveChanges();
        }
    }
}
