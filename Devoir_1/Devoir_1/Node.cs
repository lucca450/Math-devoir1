using System.Collections.Generic;

namespace Devoir_1
{
    class Node
    {
        public char letter;
        public bool isFinal;
        public List<Link> links = new List<Link>();

        public Node(char letter, bool isFinal)
        {
            this.letter = letter;
            this.isFinal = isFinal;
        }

    }
}
