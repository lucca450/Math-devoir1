using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Devoir_1
{
    class Robot
    {
        public Grammar grammar;
        private List<Node> nodes = new List<Node>();
        private int nbWorkingPaths = 0;

        public Robot(Grammar grammar)
        {
            this.grammar = grammar;
        }

        public void Execute()
        {
            BuildNodeList();                                        // Crée la liste de noeuds
            MyConsole.DisplayNodes(nodes);
        }

        private Node GetFinalNode()                             //  retourne (si trouvé, sinon le crée) le noeud de fin
        {
            foreach(Node n in nodes)
                if(n.isFinal && n.letter == 'Z')
                    return n;

            Node finalNode = new Node('Z', true);
            nodes.Add(finalNode);

            return finalNode;
        }

        private Node GetNodeWithLetter(char letter)                //  Retourne (si trouvé, sinon le crée) le noeud avec une certaine lettre 
        {
            foreach(Node n in nodes)
                if (n.letter == letter)
                    return n;

            Node newNode = new Node(letter, false);
            nodes.Add(newNode);
            return newNode;
        }
        
        private void BuildNodeList()                                // Génère la liste de noeuds selon règle
        {
            Node finalNode, nextNode;
            char firstLetter, terminal, nextLetter;

            Node firstNode = new Node('S', false);                  // Noeud d'entrée
            nodes.Add(firstNode);

            Node currentNode;
            foreach (string rule in grammar.rules)                                  //  Pour chaque règle de la grammaire
            {
                bool sRule, noLetterRule, normalRule, emptySRule, noLetterSRule;

                noLetterSRule = Regex.IsMatch(rule, "^(?:[S]->[0-1]{1})$");             //  Détecte quelle type de règle
                sRule = Regex.IsMatch(rule, "^(?:[S]->[0-1]{1}[A-Z]{1})$");
                noLetterRule = Regex.IsMatch(rule, "^(?:[A-Y]->[0-1]{1})$");
                normalRule = Regex.IsMatch(rule, "^(?:[A-Y]->[0-1]{1}[A-Y]{1})$");
                emptySRule = Regex.IsMatch(rule, "^(?:S->e)$");

                firstLetter = rule[0];                                                  //  Extrait la lettre du noeud de départ
                terminal = rule[3];                                                     //  Extrait le terminal de la règle

                switch (true)
                {
                    case true when noLetterRule || noLetterSRule:                           // Si : X->1 ou S->1
                        currentNode = GetNodeWithLetter(firstLetter);
                        finalNode = GetFinalNode();
                        currentNode.links.Add(new Link(terminal, finalNode));               //  Ajout d'un nouveau lien au noeud
                        break;
                    case true when normalRule || sRule:                                     // Si : S->1S ou S->1X ou X->1S
                        nextLetter = rule[4];

                        currentNode = GetNodeWithLetter(firstLetter);
                        nextNode = GetNodeWithLetter(nextLetter);

                        currentNode.links.Add(new Link(terminal, nextNode));                //  Ajout d'un nouveau lien au noeud
                        break;
                    case true when emptySRule:                                              // Si : S->e
                        firstNode.isFinal = true;                                               // noeud S setté à final
                        break;
                }
            }
        }

        public int CountWorkingPaths(string terminals)                                      //  Calcule le nombre de chemin possible selon les terminaux et le retourne 
        {
            CheckNode(GetNodeWithLetter('S'), terminals, -1);                               //  Commence avec le premier noeud

            return nbWorkingPaths;
        }

        /*
            Fonction récurcive
            À chaque fois qu'elle trouve un lien avec le bon terminal, elle suit le lien en rappelant la fonction avec le prochain noeud et en incrémentant l'index du terminal où on est rendu
            Si elle trouve un noeud final et qu'il n'y a pas d'autre terminal, elle incrémente le nombre de chemin possible
            sinon, elle revient d'un noeud et essaye un autre lien, ainsi de suite.
         */
        private void CheckNode(Node nodeToCheck, string terminals, int idx)                 
        {
            idx++;
            if (nodeToCheck.isFinal && idx == terminals.Length)
                nbWorkingPaths++;

            if (idx < terminals.Length)
                foreach (Link link in nodeToCheck.links)
                    if (link.terminal == terminals[idx])
                        CheckNode(link.nextNode, terminals, idx);

        }
    }
}
