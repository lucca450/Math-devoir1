namespace Devoir_1
{
    class Link
    {
        public char terminal;   //  Terminal du lien
        public Node nextNode;   //  Noeud pointé par le lien

        public Link(char terminal, Node nextNode)
        {
            this.terminal = terminal;
            this.nextNode = nextNode;
        }
    }
}
