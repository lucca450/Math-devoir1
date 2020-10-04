using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Devoir_1
{
    class Robot
    {
        public Grammar grammar;

        public Robot(Grammar grammar)
        {
            this.grammar = grammar;
        }

        public void Execute()
        {
            Node firstNode = BuildNodeList();


        }

        private Node BuildNodeList()
        {
            char firstLetter, nextLetter, terminal;

            foreach(string rule in grammar.rules)
            {
                bool sRule, noLetterRule, normalRule, emptySRule, noLetterSRule;

                noLetterSRule = Regex.IsMatch(rule, "^(?:[S]->[0-1]{1})$");
                sRule = Regex.IsMatch(rule, "^(?:[S]->[0-1]{1}[A-Z]{1})$");
                noLetterRule = Regex.IsMatch(rule, "^(?:[A-Z]->[0-1]{1})$");
                normalRule = Regex.IsMatch(rule, "^(?:[A-Z]->[0-1]{1}[A-Z]{1})$");
                emptySRule = Regex.IsMatch(rule, "^(?:S->e)$");

                switch (true)
                {
                    case true when noLetterSRule :
                        int i = 0;
                        break;
                    case true when sRule:
                        int h = 0;
                        break;
                    case true when noLetterRule && !noLetterSRule:
                        int j = 0;
                        break;
                    case true when normalRule && !sRule:
                        int k = 0;
                        break;
                    case true when emptySRule:
                        int l = 0;
                        break;
                }
            }
            return null;
        }
    }
}
