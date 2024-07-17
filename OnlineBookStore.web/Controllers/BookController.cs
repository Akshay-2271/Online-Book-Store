using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.web.Data;
using OnlineBookStore.web.Models;
using OnlineBookStore.web.Models.Entities;

namespace OnlineBookStore.web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public BookController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
                
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBooksViewModel viewModel) {
            var book = new Book
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Author = viewModel.Author
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var books = await dbContext.Books.FindAsync(id);
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book viewModel)
        {
            var books = await dbContext.Books.FindAsync(viewModel.Id);

            if(books != null)
            {
                books.Title = viewModel.Title;
                books.Description = viewModel.Description;      
                books.Author = viewModel.Author;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Book viewModel)
        {
            var books = await dbContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            
            if(books is not null)
            {
                dbContext.Books.Remove(viewModel);
                await dbContext.SaveChangesAsync();
                
            }
            return RedirectToAction("List", "Book");
        }
    }
}
