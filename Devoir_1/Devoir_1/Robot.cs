using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Devoir_1
{
    class Robot
    {
        public Grammar grammar;
        private List<Node> nodes = new List<Node>();

        public Robot(Grammar grammar)
        {
            this.grammar = grammar;
        }

        public void Execute()
        {
            BuildNodeList();
            MyConsole.DisplayNodes(nodes);

            MyConsole.WaitForAnyInput();
        }

        private Node LookForFinalNode()
        {
            foreach(Node n in nodes)
            {
                if(n.isFinal && n.letter == 'Z')
                {
                    return n;
                }
            }

            Node finalNode = new Node('Z', true);
            nodes.Add(finalNode);

            return finalNode;
        }

        private Node FindNodeWithLetter(char letter)
        {
            foreach(Node n in nodes)
                if (n.letter == letter)
                    return n;

            Node newNode = new Node(letter, false);
            nodes.Add(newNode);
            return newNode;
        }
        
        private void BuildNodeList()
        {
            Node finalNode, nextNode;
            char firstLetter, terminal, nextLetter;

            Node firstNode = new Node('S', false);
            nodes.Add(firstNode);

            Node currentNode;
            foreach (string rule in grammar.rules)
            {
                bool sRule, noLetterRule, normalRule, emptySRule, noLetterSRule;

                noLetterSRule = Regex.IsMatch(rule, "^(?:[S]->[0-1]{1})$");
                sRule = Regex.IsMatch(rule, "^(?:[S]->[0-1]{1}[A-Z]{1})$");
                noLetterRule = Regex.IsMatch(rule, "^(?:[A-Y]->[0-1]{1})$");
                normalRule = Regex.IsMatch(rule, "^(?:[A-Y]->[0-1]{1}[A-Y]{1})$");
                emptySRule = Regex.IsMatch(rule, "^(?:S->e)$");

                firstLetter = rule[0];
                terminal = rule[3];

                switch (true)
                {
                    case true when noLetterRule || noLetterSRule:                           // X->1 ou S->1
                        currentNode = FindNodeWithLetter(firstLetter);
                        finalNode = LookForFinalNode();
                        currentNode.links.Add(new Link(terminal, finalNode.letter));
                        break;
                    case true when normalRule || sRule:                                     // S->1S ou S->1X ou X->1S
                        nextLetter = rule[4];

                        currentNode = FindNodeWithLetter(firstLetter);
                        nextNode = FindNodeWithLetter(nextLetter);

                        currentNode.links.Add(new Link(terminal, nextNode.letter));
                        break;
                    case true when emptySRule:                                              // S->e
                        firstNode.isFinal = true;
                        break;
                }
            }
        }
    }
}
