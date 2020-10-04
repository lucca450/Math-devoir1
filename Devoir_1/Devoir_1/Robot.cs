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
            //Node firstNode = BuildNodeList();
            List<Node> nodesList = BuildNodeList();

        }

        private List<Node> BuildNodeList()
        {
            char firstLetter, nextLetter, terminal;
            List<Node> myNodes = new List<Node>();

            foreach (string rule in grammar.rules)
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
                        Node n1 = new Node(rule[0],false,new Link(rule[0], rule[3] , char.Parse(rule.Substring(rule.Length - 1))));
                        myNodes.Add(n1);

                        break;
                    case true when noLetterRule && !noLetterSRule:
                        int j = 0;
                        Node n2 = new Node(rule[0],false, new Link(rule[0], rule[3], 'F'));
                        myNodes.Add(n2);
                        break;
                    case true when normalRule && !sRule:
                        int k = 0;
                        Node n3 = new Node(rule[0], false, new Link(rule[0], rule[3], char.Parse(rule.Substring(rule.Length - 1))));
                        myNodes.Add(n3);
                        break;
                    case true when emptySRule:
                        int l = 0;
                        //je pense que lorsqu'on a un S->e, sans aucun autre S->XY, ca veut dire que la grammaire est tjrs e(vide) donc S sera relié à aucun noeud.
                        break;
                }
            }

            List<Node> mySnodeRules = new List<Node>();
          /*  foreach (var node in myNodes)
            {
                if (node.from == 'S')
                {
                    mySnodeRules.Add(node);
                }
                Console.WriteLine(node.from + " " + node.road + " " + node.to + " " + node.final + "\n");
            }*/
            return null;
        }
    }
}
