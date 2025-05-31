using Bookorma.Context;
using Bookorma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookorma.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context) {
        
            _context = context;
        
        }

        // GET: Book
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var books = from b in _context.books
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Bookname.Contains(searchString) || b.Author.Contains(searchString));
            }

            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) return NotFound();
            var book = await _context.books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null) return NotFound();
        
        return View(book);
        }
        // GET: Todos/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!string.IsNullOrWhiteSpace(book.Bookname))
            {
                _context.books.Add(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

     


        // GET: book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.books.FindAsync(id);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.books.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.books.FindAsync(id);
            _context.books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
