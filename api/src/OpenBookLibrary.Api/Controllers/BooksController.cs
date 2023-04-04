using Microsoft.AspNetCore.Mvc;
using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Services;
using OpenBookLibrary.Contracts.Requests;

namespace OpenBookLibrary.Api.Controllers;

[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IOpenLibraryService _openLibraryService;

    public BooksController(IBookService bookService, IOpenLibraryService openLibraryService)
    {
        _bookService = bookService;
        _openLibraryService = openLibraryService;
    }

    [HttpPost(ApiEndpoints.Books.Create)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request, CancellationToken token)
    {
        var openBook = await _openLibraryService.GetBookByIsbn13(request.Isbn13);

        if (openBook is null) return NotFound();
        var book = request.MapToBook(openBook.Title);

        await _bookService.CreateAsync(book, token);

        var bookResponse = book.MapToResponse();

        return CreatedAtAction(nameof(Get), new { id = book.Id }, bookResponse);
    }

    [HttpGet(ApiEndpoints.Books.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var book = await _bookService.GetByIdAsync(id, token);
        if (book is null) return NotFound();

        var response = book.MapToResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Books.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var books = await _bookService.GetAllAsync(token);

        var booksResponse = books.MapToResponse();
        return Ok(booksResponse);
    }

    [HttpDelete(ApiEndpoints.Books.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string idOrIsbn13, CancellationToken token)
    {
        if (Guid.TryParse(idOrIsbn13, out var id))
            await _bookService.DeleteById(id, token);
        else
            await _bookService.DeleteByIsbn13(idOrIsbn13, token);

        return Ok();
    }
}