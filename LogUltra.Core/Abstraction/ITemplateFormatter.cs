using Microsoft.Extensions.Logging;
using System;

namespace LogUltra.Core.Abstraction
{
    public interface ITemplateFormatter
    {
        string Parse<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter, string name, string[] template);
    }
}