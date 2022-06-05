using System;
using System.Collections.Generic;
using System.Text;

namespace LogUltra.Core.Abstraction
{
    public interface ITemplateParser
    {
        string[] GetTemplate(string path);
    }
}
