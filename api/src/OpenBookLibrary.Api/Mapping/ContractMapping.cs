using OpenBookLibrary.Application.Models;
using OpenBookLibrary.Contracts.Requests;
using OpenBookLibrary.Contracts.Responses;

namespace OpenBookLibrary.Api.Mapping;

public static class ContractMapping
{
    public static CreateBookModel MapToCreateBookModel(this CreateBookRequest request)
    {
        return new CreateBookModel
        {
            Isbn13 = request.Isbn13
        };
    }

    public static BookResponse MapToResponse(this Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Isbn13 = book.Isbn13,
            Title = book.Title,
            Authors = book.Authors
        };
    }

    public static BooksResponse MapToResponse(this IEnumerable<Book> books,
        int page, int pageSize, int totalCount)
    {
        return new BooksResponse
        {
            Items = books.Select(MapToResponse),
            Page = page,
            PageSize = pageSize,
            Total = totalCount
        };
    }

    public static GetAllBooksOptions MapToOptions(this GetAllBooksRequest request)
    {
        return new GetAllBooksOptions
        {
            Title = request.Title,
            Isbn13 = request.Isbn13,
            Author = request.Author,
            SortField = request.SortBy?.Trim('+', '-'),
            SortOrder = request.SortBy is null ? SortOrder.Unsorted :
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
            Page = request.Page.GetValueOrDefault(PagedRequest.DefaultPage),
            PageSize = request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize)
        };
    }

    public static IEnumerable<BorrowResponse> MapToResponse(this IEnumerable<Borrow> borrows)
    {
        return borrows.Select(x => new BorrowResponse
        {
            Borrowed = x.Borrowed,
            BookId = x.BookId,
            Returned = x.Returned
        });
    }
}