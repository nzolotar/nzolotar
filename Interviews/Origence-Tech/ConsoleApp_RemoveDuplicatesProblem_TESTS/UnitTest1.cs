using Xunit;
using ConsoleApp2;
using System;

public class StringUtilsTests
{
    [Fact]
    public void RemoveDuplicates_WithNullString_ThrowsException()
    {
        // Arrange
        string input = null;

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => Program.RemoveDuplicates(input));
        Assert.Equal("No word work with", exception.Message);
    }

    [Fact]
    public void RemoveDuplicates_WithEmptyString_ThrowsException()
    {
        // Arrange
        string input = "";

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => Program.RemoveDuplicates(input));
        Assert.Equal("No word work with", exception.Message);
    }

    [Fact]
    public void RemoveDuplicates_WithWhitespace_ThrowsException()
    {
        // Arrange
        string input = "   ";

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => Program.RemoveDuplicates(input));
        Assert.Equal("No word work with", exception.Message);
    }

    [Theory]
    [InlineData("hello", "helo")]
    [InlineData("programming", "progamin")]
    [InlineData("aabbcc", "abc")]
    [InlineData("abcabc", "abc")]
    [InlineData("a", "a")]
    public void RemoveDuplicates_WithValidString_RemovesDuplicates(string input, string expected)
    {
        // Act
        string result = Program.RemoveDuplicates(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123321", "123")]
    [InlineData("11223344", "1234")]
    public void RemoveDuplicates_WithNumbers_RemovesDuplicates(string input, string expected)
    {
        // Act
        string result = Program.RemoveDuplicates(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("!@#!@#", "!@#")]
    [InlineData("$$%%^^", "$%^")]
    public void RemoveDuplicates_WithSpecialCharacters_RemovesDuplicates(string input, string expected)
    {
        // Act
        string result = Program.RemoveDuplicates(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void RemoveDuplicates_WithMixedCharacters_RemovesDuplicates()
    {
        // Arrange
        string input = "Hello123!@#Hello123!@#";
        string expected = "Helo123!@#";

        // Act
        string result = Program.RemoveDuplicates(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void RemoveDuplicates_PreservesOrder()
    {
        // Arrange
        string input = "baacd";
        string expected = "bacd";

        // Act
        string result = Program.RemoveDuplicates(input);

        // Assert
        Assert.Equal(expected, result);
    }
}