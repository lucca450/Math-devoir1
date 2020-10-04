using System;
using System.Collections.Generic;
using System.Text;

namespace Devoir_1
{
    class Node
    {
        public Circle circle;
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
    }
}
