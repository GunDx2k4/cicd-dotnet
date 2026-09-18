using CiCd.Common.Extensions;

namespace CiCd.Common.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("   ", "")]
    public void ToSlug_WithNullOrWhiteSpace_ReturnsEmptyString(string? input, string expected)
    {
        // Act
        var result = input!.ToSlug();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("hello world", "hello-world")]
    [InlineData("DotNet 10 CI CD", "dotnet-10-ci-cd")]
    [InlineData("Already-Slugged", "already-slugged")]
    public void ToSlug_WithNormalText_ReturnsExpectedSlug(string input, string expected)
    {
        // Act
        var result = input.ToSlug();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Hello, World! 123", "hello-world-123")]
    [InlineData("Special @#$% Characters", "special--characters")]
    [InlineData("C# & ASP.NET Core", "c--aspnet-core")]
    public void ToSlug_WithSpecialCharacters_RemovesInvalidCharacters(string input, string expected)
    {
        // Act
        var result = input.ToSlug();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("   spaced   out   ", "spaced-out")]
    [InlineData("multiple    spaces    between", "multiple-spaces-between")]
    public void ToSlug_WithExtraSpaces_CollapsesAndTrimsHyphens(string input, string expected)
    {
        // Act
        var result = input.ToSlug();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("   ", "   ")]
    [InlineData("invalid-email-no-at-sign", "invalid-email-no-at-sign")]
    public void MaskEmail_WithInvalidOrEmptyEmail_ReturnsOriginalInput(string? input, string? expected)
    {
        // Act
        var result = input!.MaskEmail();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("a@example.com", "a*@example.com")]
    [InlineData("ab@example.com", "a*@example.com")]
    public void MaskEmail_WithNameLengthLessThanOrEqualToTwo_MasksWithOneStar(string input, string expected)
    {
        // Act
        var result = input.MaskEmail();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("abc@example.com", "a*c@example.com")]
    [InlineData("john@example.com", "j**n@example.com")]
    [InlineData("admin@example.com", "a***n@example.com")]
    [InlineData("nguyenvana@gmail.com", "n********a@gmail.com")]
    public void MaskEmail_WithNameLengthGreaterThanTwo_MasksMiddleCharacters(string input, string expected)
    {
        // Act
        var result = input.MaskEmail();

        // Assert
        Assert.Equal(expected, result);
    }
}
