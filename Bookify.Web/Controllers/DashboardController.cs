namespace Bookify.Web.Controllers
{
    //[Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DashboardController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            //var numberOfCopies = _context.BookCopies.Count(c => !c.IsDeleted);
            var numberOfCopies = _context.Books.Count(c => !c.IsDeleted);
            //for making number of copies like that 

            //numberOfCopies = numberOfCopies <= 10 ? numberOfCopies : (float)numberOfCopies / 10 * 10;
            numberOfCopies = numberOfCopies <= 10 ? numberOfCopies : numberOfCopies / 10 * 10;  //same result if i use this --->> numberOfCopies = numberOfCopies <= 10 ? numberOfCopies : (int)numberOfCopies / 10 * 10;

            var numberOfsubscribers = _context.Subscribers.Count(c => !c.IsDeleted);
            var lastAddedBooks = _context.Books
                                .Include(b => b.Author)
                                .Where(b => !b.IsDeleted)
                                .OrderByDescending(b => b.Id)
                                .Take(8)
                                .ToList();

            var topBooks = _context.RentalCopies
                .Include(c => c.BookCopy)
                .ThenInclude(c => c!.Book)
                .ThenInclude(b => b!.Author)
                .GroupBy(c => new
                {
                   BookId= c.BookCopy!.BookId,
                   Title = c.BookCopy!.Book!.Title,
                   ImageThumbnailUrl=c.BookCopy!.Book!.ImageThumbnailUrl,
                   AuthorName = c.BookCopy!.Book!.Author!.Name
                })
                .Select(b => new //when we use group by we must use select plz review group by in entity framework
                {
                    b.Key.BookId, // "key" is the name of gorup by and we have many groupby parameter in object then the key "nameof group by parameter" has many values
                    b.Key.Title,
                    b.Key.ImageThumbnailUrl,
                    b.Key.AuthorName,
                    Count = b.Count()  //groupby has two porpertise when we use select "key and count"
                })
                .OrderByDescending(b => b.Count)//i will make order
                .Take(6)// will take the bigest 6 values 
                .Select(b => new BookViewModel
                {
                    Id = b.BookId,
                    Title = b.Title,
                    ImageThumbnailUrl = b.ImageThumbnailUrl,
                    Author = b.AuthorName
                })
                .ToList();

            var viewModel = new DashboardViewModel
            {
                NumberOfCopies = numberOfCopies,
                NumberOfSubscribers = numberOfsubscribers,
                LastAddedBooks = _mapper.Map<IEnumerable<BookViewModel>>(lastAddedBooks),
                TopBooks = topBooks
            };

            return View(viewModel);
        }

        [AjaxOnly]
        public IActionResult GetRentalsPerDay(DateTime? startDate, DateTime? endDate)
        {
            var number = IEnumerable.range(10,15);
            startDate ??= DateTime.Today.AddDays(-29);
            endDate ??= DateTime.Today;

            var data = _context.RentalCopies
                .Where(c => c.RentalDate >= startDate && c.RentalDate <= endDate)
                .GroupBy(c => new { Date = c.RentalDate })
                .Select(g => new ChartItemViewModel
                {
                    Label = g.Key.Date.ToString("d MMM"),
                    Value = g.Count().ToString()
                })
                .ToList();

            /*
			List<ChartItemViewModel> figures = new ();

            for (var day = startDate; day <= endDate; day = day.Value.AddDays(1))
            {
                var dayData = data.SingleOrDefault(d => d.Label == day.Value.ToString("d MMM"));

                ChartItemViewModel item = new()
                {
                    Label = day.Value.ToString("d MMM"),
                    Value = dayData is null ? "0" : dayData.Value
				};

                figures.Add(item);
            }
            */

            return Ok(data);
        }

        [AjaxOnly]
        public IActionResult GetSubscribersPerCity()
        {
            var data = _context.Subscribers
                .Include(s => s.Governorate)
                .Where(s => !s.IsDeleted)
                .GroupBy(s => new { GovernorateName = s.Governorate!.Name })
                .Select(g => new ChartItemViewModel
                {
                    Label = g.Key.GovernorateName,
                    Value = g.Count().ToString()
                })
                .ToList();

            return Ok(data);
        }
    }
}