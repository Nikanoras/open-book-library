using FluentAssertions;
using OpenBookLibrary.Domain.Books;

namespace OpenBookLibrary.Domain.UnitTests.Books;

[TestFixture]
public class BookTests
{
    [Test]
    public void ShouldHaveTheMandatoryData()
    {
        var isbn13 = new Isbn13(9780132350884);
        const string title = "Clean Code";
        const string author = "Robert C. Martin";

        var book = new Book(isbn13, title, new[] { author });
        
        book.Isbn13.Should().Be(isbn13);
        book.Title.Should().Be(title);
        book.Authors.Should().Contain(author);
    }
}