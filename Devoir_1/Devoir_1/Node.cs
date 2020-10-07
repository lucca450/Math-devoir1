using System;
using System.Collections.Generic;
using System.Text;

namespace Devoir_1
{
    class Node
    {
        /* public Circle circle;
         public List<Link> nexts = new List<Link>();

         public Node(char circleLetter, char terminal, char nextCircleLetter)
         {
             circle = new Circle(circleLetter);
             nexts.Add(new Link(terminal, nextCircleLetter));

         }

         public Node(char nextCircleLetter)
         {
             circle = new Circle(nextCircleLetter);
         }
         */
        public char letter;
        public bool isFinal;
        public List<Link> links = new List<Link>();



        public Node(char letter, bool isFinal, Link link)
        {
            this.letter = letter;
            this.isFinal = isFinal;
            links.Add(link);
        }

        public Node(char letter, bool isFinal)
        {
            this.letter = letter;
            this.isFinal = isFinal;
        }

    }
}
