using FluentAssertions;
using OpenBookLibrary.Domain.Books;

namespace OpenBookLibrary.Domain.UnitTests.Books;

[TestFixture]
public class Isbn13Tests
{
    [Test]
    public void ShouldBeAbleToCreate()
    {
        var isbn13 = new Isbn13(9780132350884);

        isbn13.Value.Should().Be(9780132350884);
    }

    [Test]
    public void ShouldStartWith978()
    {
        var createIsbn13 = () => new Isbn13(6240132350884);

        createIsbn13.Should().Throw<ArgumentOutOfRangeException>().WithMessage("ISBN-13 must start with 978 (Parameter 'value')");
    }

    [TestCase(978013235084)]
    [TestCase(978123)]
    [TestCase(97812345678901234)]
    public void ShouldBe13DigitsLength(long number)
    {
        var createIsbn13 = () => new Isbn13(number);

        createIsbn13.Should().Throw<ArgumentOutOfRangeException>().WithMessage("ISBN-13 must have exact length of 13 digits (Parameter 'value')");
    }
}