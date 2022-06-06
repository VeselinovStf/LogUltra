using LogUltra.Core.Abstraction.Format;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogUltra.TemplateParser
{
    public class TemplateFormatter : ITemplateFormatter
    {
        public string Parse<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter,
            string name,
            string[] template)
        {
            Dictionary<string, string> templateMappingData = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "eventId", eventId.Id.ToString() },
                { "logLevel", logLevel.ToString() },
                { "name", name },
                { "formatter", formatter(state, exception) }
            };

            var resultBuilder = new StringBuilder();

            for (int i = 0; i < template.Length; i++)
            {
                var row = template[i];

                var contained = false;
                foreach (var t in templateMappingData)
                {
                    if (row.Contains(t.Key))
                    {
                        resultBuilder.AppendLine(row.Replace(row, t.Value));
                        contained = true;
                    }                    
                }

                if (!contained)
                {
                    resultBuilder.AppendLine(row);
                }
            }

            return resultBuilder.ToString();
        }
    }
}
