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

                        if (myNodes.Count != 0)
                        {
                            foreach (var node in myNodes)
                            {
                                if (rule[0] == node.letter)
                                {
                                    node.nodeLinks.Add(new Link (rule[0], rule[3], char.Parse(rule.Substring(rule.Length - 1))));
                                }
                                else
                                {
                                    Node n1 = new Node(rule[0],false,new Link(rule[0], rule[3] , char.Parse(rule.Substring(rule.Length - 1))));
                                    myNodes.Add(n1);
                                }
                                break;
                            }
                        }
                        else
                        {
                            Node n1 = new Node(rule[0], false, new Link(rule[0], rule[3], char.Parse(rule.Substring(rule.Length - 1))));
                            myNodes.Add(n1);
                        }



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



           //Console.WriteLine( String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", myNodes[0].letter, myNodes[1].letter, myNodes[2].letter, myNodes[3].letter));


            foreach (var node in myNodes)
            {
               //myNodes node.nodeLinks


                Console.WriteLine(node.letter + ": ");
                foreach (var link in node.nodeLinks)
                {
                    Console.WriteLine("From " + link.from + " using " + link.road + " to " + link.to + ": ");
                }
            }



            /*
            foreach (var link in node.nodeLinks)
            {
                Console.WriteLine(link.from + " " + link.road + " " + link.to + ": \n");
            }
            */


           
            return null;
        }
    }
}
