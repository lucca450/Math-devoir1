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
        public char terminal;
        public char to;

        public Link(char terminal, char to)
        {
            this.terminal = terminal;
            this.to = to;
        }
    }
}
