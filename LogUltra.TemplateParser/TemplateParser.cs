using LogUltra.Core.Abstraction;
using System;
using System.IO;

namespace LogUltra.TemplateParser
{
    public class TemplateParser : ITemplateParser
    {
        public string[] GetTemplate(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileLoadException($"Template not found: {path}");
            }

            return File.ReadAllLines(path);
        }
    }
}
