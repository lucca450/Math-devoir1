using System;
using System.Collections.Generic;
using System.Text;

namespace Devoir_1
{
    class Link
    {
        /*
        public char terminal;
        public Node node;

        public Link(char terminal, char nextCircleLetter)
        {
            this.terminal = terminal;
            this.node = new Node(nextCircleLetter);
        }
        */
        public char from;
        public char road;
        public char to;

        public Link(char from, char road, char to)
        {
            this.from = from;
            this.road = road;
            this.to = to;
        }
    }
}
