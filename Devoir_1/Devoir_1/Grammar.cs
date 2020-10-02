using System;
using System.Collections.Generic;
using System.Text;

namespace Devoir_1
{
    class Grammar
    {
        public List<string> rules;
        public string fileName;
        public string path;

        public Grammar(string fileName, string path, List<string> rules)
        {
            this.fileName = fileName;
            this.path = path;
            this.rules = rules;
        }
    }
}
