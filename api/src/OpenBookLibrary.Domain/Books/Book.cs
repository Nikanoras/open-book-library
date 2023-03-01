namespace OpenBookLibrary.Domain.Books;

public class Book
{
    private readonly string[] _authors;

    public Book(Isbn13 isbn13, string title, string[] authors)
    {
        _authors = authors;
        Isbn13 = isbn13;
        Title = title;
    }

    public Isbn13 Isbn13 { get; }
    public string Title { get; }

    public string[] Authors => _authors.ToArray();
}