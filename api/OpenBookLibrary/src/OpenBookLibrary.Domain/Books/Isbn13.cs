namespace OpenBookLibrary.Domain.Books;

public struct Isbn13
{
    public long Value { get; }

    public Isbn13(long value)
    {
        var str = value.ToString();
        
        if (!str.StartsWith("978")) 
            throw new ArgumentOutOfRangeException(nameof(value), "ISBN-13 must start with 978");

        if (str.Length != 13)
            throw new ArgumentOutOfRangeException(nameof(value), "ISBN-13 must have exact length of 13 digits");

        Value = value;
    }
}