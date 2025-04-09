using System.Diagnostics.CodeAnalysis;

namespace TaskManager.Application.Exceptions;

[ExcludeFromCodeCoverage(Justification = "This is a custom exception class and does not require unit tests.")]
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}

