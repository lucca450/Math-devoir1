using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Devoir_1
{
    static class MyConsole
    {
        public static string AskFileName(bool isImport = false)
        {
            string fileName = "", path;
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Entrez le nom du fichier: ");
                fileName = Console.ReadLine().Trim();                        //  Lecture du nom du fichier                                        //  Lecture du nom du fichier
                path = MyFileManager.FormatPath(fileName);                      //  Formatage du chemin du fichier

                if (isImport)
                    done = MyFileManager.FileExists(path);                                   //  Vérification de l'existance du fichier
                else
                    done = !MyFileManager.FileExists(path);                                   //  Vérification de l'existance du fichier

                if (!done)                                                                      //  Si pas fini
                    if (isImport)                                                                   // Si en cours d'importation
                        Console.WriteLine("Aucune grammaire trouvée.\n");
                    else
                        Console.WriteLine("Une grammaire avec ce nom existe déjà.\n");

            }
            return fileName;
        }

        public static string MainMenu(string fileName = "")
        {
            bool invalidInput = true;
            while (invalidInput)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                Console.Clear();                                                        //  Clear de la console

                if (fileName != null && fileName != "")
                    Console.WriteLine("Grammaire importée : " + fileName + "\n");
                else
                    Console.WriteLine("Aucune grammaire importée");

                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu principal de l'application
                Console.WriteLine("1- Créer une nouvelle grammaire");
                Console.WriteLine("2- Charger une grammaire existante");
                Console.WriteLine("3- Visualiser la grammaire actuelle");
                Console.WriteLine("4- Executer l'automate");
                Console.WriteLine("5- Quitter");

                string input = Console.ReadLine();                                      //  Lecture de la réponse

                switch (input)
                {
                    case "1":                                                               // Si 1 : Création de la grammaire
                    case "2":                                                               // Si 2 : Importation de la grammaire
                    case "3":                                                               // Si 3 : Visualisation de la grammaire
                    case "4":                                                               // Si 4 : Exécution de l'automate
                    case "5":                                                               // Si 5 : Fermeture de l'application
                        return input;
                    default:                                                                // Si entrée invalide
                        invalidInput = true;
                        Console.WriteLine("Entrée invalide");
                        break;
                }
            }
            return "-1";
        }   

        public static void DisplayWorkingPaths(int nbWorkingPaths, string terminals)    //  Affichage des résultats des terminaux selon la grammaire
        {
            Console.WriteLine(string.Format("Les terminaux {0} ont généré {1} chemin(s) valide(s) !", terminals, nbWorkingPaths));
        }

        public static string AskTerminalInput()
        {
            string terminals = "";
            Console.Write("Entrer la liste des terminaux (1 ou 0): ");
            bool ok = false;

            while (!ok)                                                     //  Tant que pas ok
            {
                ok = true;
                terminals = Console.ReadLine();                                 //  Lecture de l'entrée

                foreach(char c in terminals)
                    if(c != '0' && c != '1')                                    //  Vérification de l'entrée si c'est seulement des 1 ou des 0
                        ok = false;
                    
                if (terminals.Equals(""))                                       //  Autorisation de ne rien entrée
                    ok = true;

                if (!ok)
                    Console.Write("Ce n'est pas valide. Entrer la liste des terminaux (1 ou 0): ");
            }

            return terminals;                                              // Retourne les terminaux
        }

        public static void WaitForAnyInput()                                //  En attente de l'utilisateur
        {
            Console.WriteLine("\nAppuyer sur une touche pour continuer\n");
            Console.ReadLine();
        }

        public static void SuccessfulImport()                               //  Succès importation
        {
            Console.WriteLine("Importation réussie");
        }

        public static void NoRuleToDelete()                               //  Aucune règle à supprimer
        {
            Console.WriteLine("Il n'y a aucune règle à supprimer.");
        }

        public static void InvalidImport()                                  //  Impossible d'importer
        {
            Console.WriteLine("La grammaire n'a pas été importée. Vérifiez vos régles, car elles ne respectent pas le format demandé.");
        }

        public static void MustImportGrammarFirst()                         // Doit importer une grammaire d'abord
        {
            Console.WriteLine("Veuillez d'abord importer une grammaire");
        }

        public static string AskRule(Grammar grammar)                       //  Demande d'une règle
        {
            string rule = "";

            bool validRule = false;
            while (!validRule)
            {
                Console.WriteLine("Entrez une règle à la grammaire. Respectez le format suivant: S->e OU X->1 OU X->1X  *Les lettres de A à Y sont acceptées.*");
                rule = Console.ReadLine().Trim();                                                                               //  Entrée de la règle

                if (Regex.IsMatch(rule, "^(?:[A-Y]->[0-1]{1}[A-Y]{1}|[A-Y]->[0-1]{1}|S->e)"))                                       // Si le format convient 
                {
                    if (!grammar.rules.Contains(rule))                                                                              //  
                    {
                        validRule = true;
                    }
                    else
                        Console.WriteLine("La règle existe déjà");
                }
                else
                {
                    Console.WriteLine("La règle n'est pas valide. Respectez le format suivant: S->e OU X->1 OU X->1X *Les lettres de A à Y sont acceptées.*");
                }
            }

            return rule;
        }

        public static string AskForMenuSelection()
        {
            bool invalidInput = true, done = false;
            while (invalidInput || !done)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                Console.WriteLine("Choisissez parmis les fonctions suivantes :");                   //  Menu de l'application
                Console.WriteLine("1- Modifier une regle");
                Console.WriteLine("2- Supprimer une regle");
                Console.WriteLine("3- Ajouter une ou plusieurs regles");
                Console.WriteLine("4- Quitter");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":                                                               // Si 1 : Modification d'une regle
                    case "2":                                                               // Si 2 : Suppression d'une regle
                    case "3":                                                               // Si 3 : Ajout d'une ou plusieurs regles
                    case "4":                                                               // Si 4 : Quitter
                        return input;
                    default:
                        Console.WriteLine("Entrée invalide");
                        invalidInput = true;
                        break;
                }
            }
            return "";
        }

        public static void NoChange()                   
        {
            Console.WriteLine("Aucun changement");
        }                                       //  Aucun changement

        public static void YouCantChangeTheOnlySRule()
        {
            Console.WriteLine("Vous ne pouvez pas retirer la seule règle S");
        }                                       // Impossible de changer la seule règle S

        public static void Error()
        {
            Console.WriteLine("Une erreur est survenue.");
        }

        public static List<string> AskRules(Grammar grammar, bool adding = false)               //  Demande de règles
        {
            Console.WriteLine("Sachez que :\n" +
                "- S est le symbole de départ\n" +
                "- V=(S,A,B,C,D,...,Y)\n" +
                "- T=(0,1)\n" +
                "- R sont les regles");

            List<string> rules = new List<string>();

            bool done = false;
            while (!done)                                                           //  Tant que l'utilisateur n'a pas fini d'écrire de règle
            {
                string rule = AskRule(grammar);                                         //  Demande une règle
                rules.Add(rule);                                                        //  Ajoute la règle dans la liste

                Console.WriteLine(string.Format("Règle ajoutée: {0}", rule));

                bool validInput = false;
                while (!validInput)                                                     //  Tant que l'utiliseur n'a pas de entrée de donnée valide
                {
                    validInput = true;
                    int foundSRule = 0;
                    Console.WriteLine("Voulez-vous ajouter une autre règle? o/n");
                    string input = Console.ReadLine();                                      //  Entrée de l'utilisateur

                    switch (input)
                    {
                        case "o":                                                           //  Si oui
                            break;
                        case "n":                                                           //  Si non     
                            if (!adding)
                            {
                                foreach (string r in rules)                                         //Cherche une règle S
                                    if (r != "")
                                        if (r.Substring(0, 1) == "S")
                                            foundSRule++;

                                done = foundSRule > 0;
                            }
                            else
                                done = true;

                            break;
                        default:                                                            //  Si entrée invalide
                            Console.WriteLine("Entrée invalide");
                            validInput = false;
                            break;
                    }
                }
            }
            return rules;
        }

        public static string GrammarMenu()
        {
            bool invalidInput = true, done = false;
            while (invalidInput || !done)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu principal de l'application
                Console.WriteLine("1- Modifier la grammaire");
                Console.WriteLine("2- Supprimer la grammaire");
                Console.WriteLine("3- Afficher la grammaire");
                Console.WriteLine("4- Quitter");

                string input = Console.ReadLine();                                          //  Lecture de l'entrée de l'input

                switch (input)
                {
                    case "1":                                                               // Si 1 : Modification de la grammaire
                    case "2":                                                               // Si 2 : Suppression de la grammaire
                    case "3":                                                               // Si 3 : Affichage de la grammaire
                    case "4":                                                               // Si 4 : Quitter
                        return input;
                    default:
                        Console.WriteLine("Entrée invalide");
                        invalidInput = true;
                        break;
                }
            }
            return "-1";
        }

        public static bool AskToRechargeGrammar()
        {
            bool validInput = false;
            while (!validInput)                                                     //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                Console.WriteLine("Une grammaire est déjà chargée, voulez-vous en charger une nouvelle? o/n");
                string inp = Console.ReadLine();                                      //  Entrée de l'utilisateur

                switch (inp)
                {
                    case "o":                                                           //  Si oui
                        return true;
                    case "n":                                                           //  Si non
                        return false;
                    default:                                                            //  Si entrée invalide
                        Console.WriteLine("Entrée invalide");
                        validInput = false;
                        break;
                }
            }
            return false;
        }

        public static void DisplayGrammarWithOptions(Grammar grammar)                       //  Formatte les règles avec une choix
        {
            int i = 1;
            foreach (string rule in grammar.rules)
            {
                Console.WriteLine(string.Format("{0}- {1}", i, rule));                  
                i++;
            }
        }

        public static int AskForRuleSelection(string keyword, int max)                      //  Sélection de la règle
        {
            int choice = -1;
            bool invalidInput = true;
            while (invalidInput)
            {
                Console.WriteLine("Quelle regle voulez-vous " + keyword + "?");
                string input = Console.ReadLine();

                try
                {
                    choice = int.Parse(input);                                              //  Conversion de l'entrée en entier
                    invalidInput = choice < 1 || choice > max;                              //  Si le choix est plus grand que 1 et plus petit que le nombre max de règle
                    if (invalidInput)
                    {
                        Console.WriteLine("Vous devez entrer un entier entre 1 et " + max);
                    }
                }
                catch
                {
                    Console.WriteLine("Vous devez entrer un entier entre 1 et " + max);
                    invalidInput = true;
                }

            }
            return choice;
        }

        public static bool AskForConfirmation()
        {
            bool validInput = false;
            while (!validInput)                                                         //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                Console.WriteLine("Voulez-vous sauvegarder la grammaire? o/n");             //  Demande de sauvegarde de la grammaire
                string input = Console.ReadLine();                                          //  Lecture de la réponse

                switch (input)
                {
                    case "o":                                                               //  Si oui
                        return true;
                    case "n":                                                               //  Si non
                        return false;
                    default:                                                                //  Si entrée invalide
                        Console.WriteLine("Entrée invalide");
                        validInput = false;
                        break;
                }
            }
            return false;
        }

        public static void DisplayGrammar(Grammar grammar)                                  //  Affichage de chaque règle de la grammaire
        {
            Console.WriteLine("Grammaire importé : " + grammar.fileName + "\n");

            foreach (string rule in grammar.rules)
                Console.WriteLine(rule);
        }

        

        

        public static void DisplayNodes(List<Node> nodes)
        {
            ConsoleTable cT = new ConsoleTable(60);                                         // Création de la table de console
            cT.PrintLine();                                                                        
            cT.PrintRow("Etats", "Terminal 0", "Terminal 1");                               // en-tête
            cT.PrintLine();

            string terminal0, terminal1, finalNodes = "";

            foreach(Node node in nodes)                                                     //  Pour chaque noeud
            {
                if (node.isFinal)                                                           //  Si noeud est final
                    finalNodes += node.letter;
                if(node.letter != 'Z')
                {
                    terminal0 = "";
                    terminal1 = "";

                    foreach(Link link in node.links)                                        //  Pour chaque lien du noeud
                    {
                        if (link.terminal.Equals('0'))                                          //  Si le terminal du lien est 0
                            terminal0 += link.nextNode.letter;
                        else
                            terminal1 += link.nextNode.letter;                                  //  Sinon
                    }

                    terminal0 = string.Join<char>(",", terminal0);                              //  Ajout de virgure entre chaque lettre de noeud
                    terminal1 = string.Join<char>(",", terminal1);

                    cT.PrintRow(node.letter.ToString(), terminal0, terminal1);                  //  Impression de la rangée dans la table
                }
            }
            cT.PrintLine();

            Console.WriteLine("Les noeuds finaux sont : " + string.Join<char>(",", finalNodes));    //  Affichage des noeuds finaux

        }
    }
}
