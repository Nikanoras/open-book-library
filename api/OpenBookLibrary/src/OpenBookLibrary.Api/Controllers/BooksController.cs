using Microsoft.AspNetCore.Mvc;
using OpenBookLibrary.Api.Mapping;
using OpenBookLibrary.Application.Repositories;
using OpenBookLibrary.Contracts.Requests;

namespace OpenBookLibrary.Api.Controllers;

[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpPost(ApiEndpoints.Books.Create)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        var book = request.MapToBook();
        await _bookRepository.CreateAsync(book);
        var bookResponse = book.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = book.Id }, bookResponse);
    }

    [HttpGet(ApiEndpoints.Books.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        var response = book.MapToResponse();
        return Ok(response);
    }
}