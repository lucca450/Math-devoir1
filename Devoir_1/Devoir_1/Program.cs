using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Devoir_1
{
    class Program
    {
        static string FormatPath(string fileName)
        {
            return string.Format(@".\{0}.txt", fileName);                       //  Formatage du chemin du fichier 
        }
        static Grammar CreateGrammar()
        {
            bool fileExists = true;
            string path = "";
            string fileName = "";
            while (fileExists)
            {
                fileName = AskFileName();                                           //  Lecture du nom du fichier

                path = string.Format(@".\{0}.txt", fileName);                       //  Formatage du chemin du fichier

                fileExists = File.Exists(path);                                     //  Vérification de l'existance du fichier

                if (fileExists)
                    Console.WriteLine("Une grammaire avec ce nom existe déjà.\n");
            }

            Console.WriteLine("Sachez que :\n" +
                "- S est le symbole de départ\n" +
                "- V=(S,A,B,C,D,...)\n" +
                "- T=(0,1)\n" +
                "- R sont les regles");

            bool validInput;

            List<string> rules = AskRules();

            validInput = false;
            while (!validInput)                                                         //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                validInput = true;

                Console.WriteLine("Voulez-vous sauvegarder la grammaire? o/n");             //  Demande de sauvegarde de la grammaire
                string input = Console.ReadLine();                                          //  Lecture de la réponse

                switch (input)
                {
                    case "o":                                                               //  Si oui
                        WriteGrammarInFile(path, rules);
                        return new Grammar(fileName, path, rules);
                    case "n":                                                               //  Si non
                        rules.Clear();
                        break;
                    default:                                                                //  Si entrée invalide
                        Console.WriteLine("Entrée invalide");
                        validInput = false;
                        break;
                }
            }
            return null;
        }

        static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        static void WriteGrammarInFile(string path, List<string> rules, bool append = false)
        {
            StreamWriter sw = new StreamWriter(path, append) ;

            if (append)
            {
                sw.WriteLine();
            }

            int i = 0;
            foreach (string rule in rules)                                          //  Boucle dans chaque règle
            {
                i++;

                if (i != rules.Count)
                {
                    sw.WriteLine(rule);                                                     //  Écrit la règle dans le fichier
                }else
                {
                    sw.Write(rule);
                }

            }

            sw.Close();
        }

        static string AskFileName()
        {   
            Console.WriteLine("Entrez le nom du fichier: ");
            string fileName = Console.ReadLine().Trim();                        //  Lecture du nom du fichier

            return fileName;
        }

        static Grammar ImportGrammar()
        {
            bool fileExists = false;
            string fileName = "", path = "";
            while (!fileExists)
            {
                fileName = AskFileName();
                path = FormatPath(fileName);

                fileExists = FileExists(path);

                if (!fileExists)
                    Console.WriteLine("Aucune grammaire n'a été trouvée");
            }


            StreamReader sr = new StreamReader(path);

            List<string> rules = sr.ReadToEnd().Split("\r\n").ToList<string>();
            Grammar grammar = new Grammar(fileName, path, rules);

            sr.Close();

            return grammar;
        }

        private static void DisplayGrammar(Grammar grammar)
        {
            Console.WriteLine("Grammaire importé : " + grammar.fileName + "\n");

            foreach(string rule in grammar.rules)
            {
                Console.WriteLine(rule);
            }
        }

        static void Main(string[] args)
        {
            Grammar grammar = null;

            bool invalidInput = true;
            while (invalidInput)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                if (grammar != null)
                {
                    Console.WriteLine("Grammaire importée : " + grammar.fileName + "\n");
                }
                else
                {
                    Console.WriteLine("Aucune grammaire importée");
                }

                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu principal de l'application
                Console.WriteLine("1- Créer une nouvelle grammaire");
                Console.WriteLine("2- Charger une grammaire existante");
                Console.WriteLine("3- Visualiser la grammaire actuelle");
                Console.WriteLine("4- Quitter");

                string input = Console.ReadLine();                                      //  Lecture de la réponse

                switch(input)
                {
                    case "1":                                                               // Si 1 : Création de la grammaire
                        grammar = CreateGrammar();
                        break;
                    case "2":                                                               // Si 2 : Importation de la grammaire
                        if(grammar == null)
                        {
                            grammar = ImportGrammar();
                        }
                        else
                        {
                            bool validInput = false;
                            while (!validInput)                                                     //  Tant que l'utiliseur n'a pas de entrée de donnée valide
                            {
                                validInput = true;

                                Console.WriteLine("Une grammaire est déjà chargée, voulez-vous en charger une nouvelle? o/n");
                                string inp = Console.ReadLine();                                      //  Entrée de l'utilisateur

                                switch (inp)
                                {
                                    case "o":                                                           //  Si oui
                                        grammar = ImportGrammar();
                                        break;
                                    case "n":                                                           //  Si non                                
                                        break;
                                    default:                                                            //  Si entrée invalide
                                        Console.WriteLine("Entrée invalide");
                                        validInput = false;
                                        break;
                                }
                            }
                        }
                        break;
                    case "3":                                                               // Si 3 : Visualisation de la grammaire
                        if (grammar != null)
                        {
                            VisualizeGrammar(grammar);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez d'abord importer une grammaire");
                        }
                        break;
                    case "4":                                                               // Si 4 : Fermeture de l'application
                        invalidInput = false;
                        break;
                    default:                                                                // Si entrée invalide
                        Console.WriteLine("Entrée invalide");
                        break;
                }
            }
        }

        private static Grammar VisualizeGrammar(Grammar grammar)
        {
            DisplayGrammar(grammar);
            bool invalidInput = true;
            while (invalidInput)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                invalidInput = false;
                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu principal de l'application
                Console.WriteLine("1- Modifier la grammaire");
                Console.WriteLine("2- Supprimer la grammaire");
                Console.WriteLine("3- Afficher la grammaire");
                Console.WriteLine("4- Quitter");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":                                                               // Si 1 : Modification de la grammaire
                        grammar = GrammarModification(grammar);
                        break;
                    case "2":                                                               // Si 2 : Suppression de la grammaire
                        
                        break;
                    case "3":                                                               // Si 3 : Affichage de la grammaire
                        
                        break;
                    case "4":                                                               // Si 4 : Quitter
                        break;
                    default:
                        Console.WriteLine("Entrée invalide");
                        invalidInput = true;
                        break;
                }
            }
                return grammar;
        }

        private static void DisplayGrammarWithOptions(Grammar grammar)
        {
            int i = 1;
            foreach(string rule in grammar.rules)
            {
                Console.WriteLine(string.Format("{0}- {1}", i, rule));
            }
        }

        private static Grammar GrammarModification(Grammar grammar)
        {
            bool invalidInput = true;
            while (invalidInput)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                invalidInput = false;
                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu de l'application
                Console.WriteLine("1- Modifier une regle");
                Console.WriteLine("2- Supprimer une regle");
                Console.WriteLine("3- Ajouter une ou plusieurs regles");
                Console.WriteLine("4- Quitter");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":                                                               // Si 1 : Modification d'une regle
                        DisplayGrammarWithOptions(grammar);
                        break;
                    case "2":                                                               // Si 2 : Suppression d'une regle
                        DisplayGrammarWithOptions(grammar);
                        break;
                    case "3":                                                               // Si 3 : Ajout d'une ou plusieurs regles
                        List<string> addedRules = AskRules(true);

                        if (AskForConfirmation())
                        {
                            WriteGrammarInFile(grammar.path, addedRules, true);
                            grammar.rules.AddRange(addedRules);
                        }
                        break;
                    case "4":                                                               // Si 4 : Quitter
                        break;
                    default:
                        Console.WriteLine("Entrée invalide");
                        invalidInput = true;
                        break;
                }
            }

            return grammar;
        }

        private static bool AskForConfirmation()
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

        private static List<string> AskRules(bool adding = false)
        {
            List<string> rules = new List<string>();

            bool done = false;
            while (!done)                                                           //  Tant que l'utilisateur n'a pas fini d'écrire de règle
            {
                Console.WriteLine("Entrez une règle à la grammaire. Respectez le format suivant: S->e OU X->1 OU X->1X");
                string rule = Console.ReadLine().Trim();                                //  Lecture de la règle

                if (Regex.IsMatch(rule, "^(?:[A-Z]->[0-1]{1}[A-Z]{1}|[A-Z]->[0-1]{1}|S->e)"))
                {
                bool validInput;
                    rules.Add(rule);                                                        //  Ajoute la règle dans la liste

                    Console.WriteLine(string.Format("Règle ajoutée: {0}", rule));

                    validInput = false;
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
                                    {
                                        if (r != "")
                                        {
                                            if (r.Substring(0, 1) == "S")
                                            {
                                                foundSRule++;
                                            }
                                        }
                                    }
                                    done = foundSRule > 0;
                                }
                                else
                                {
                                    done = true;
                                }

                                break;
                            default:                                                            //  Si entrée invalide
                                Console.WriteLine("Entrée invalide");
                                validInput = false;
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("La règle n'est pas valide. Respectez le format suivant: S->e OU X->1 OU X->1X");
                }
            }
            return rules;
        }
    }
}
