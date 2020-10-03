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

        private static Grammar ImportGrammar()
        {
            bool fileExists = false;
            string fileName = "", path = "";
            while (!fileExists)
            {
                fileName = AskFileName();
                path = FormatPath(fileName);

                fileExists = FileExists(path);

                if (!fileExists)
                {
                    Console.WriteLine("Aucune grammaire n'a été trouvée");
                }

            }


            StreamReader sr = new StreamReader(path);

            List<string> rules = sr.ReadToEnd().Split("\r\n").ToList<string>();

            int error = 0;
            int sRuleCounter = 0;
            foreach (var rule in rules)
            {
                if (rule.Contains("S"))
                {
                    sRuleCounter++;
                }
                if (!Regex.IsMatch(rule, "^(?:[A-Z]->[0-1]{1}[A-Z]{1}|[A-Z]->[0-1]{1}|S->e)") || ImportedGrammerDuplicate(rule, rules))
                {
                    error++;
                }
            }

            if (error == 0 && sRuleCounter >= 1)
            {
                Grammar grammar = new Grammar(fileName, path, rules);
                sr.Close();
                return grammar;
            }
            else
            {
                Console.WriteLine("La grammaire n'a pas été importée. Vérifiez vos régles, car elles ne respectent pas le format demandé.");
                sr.Close();
                return null;
            }
        }

        static bool ImportedGrammerDuplicate(string rule, List<string> rules)
        {
            int duplicateRule = 0;
            foreach (var r in rules)
            {
                if (r == rule)
                {
                    duplicateRule++;
                }
            }
            if (duplicateRule == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
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
           

            Console.WriteLine("\n\n\n\n Vérifier les règles importées \n\n\n");






            Grammar grammar = null;

            bool invalidInput = true, done = false;
            while (invalidInput || !done)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                //Console.Clear();

                invalidInput = false;
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
                        done = true;
                        break;
                    default:                                                                // Si entrée invalide
                        invalidInput = true;
                        Console.WriteLine("Entrée invalide");
                        break;
                }
            }
        }

        private static Grammar VisualizeGrammar(Grammar grammar)
        {
            DisplayGrammar(grammar);
            bool invalidInput = true, done = false;
            while (invalidInput || !done)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
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
                        done = true;
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
                i++;
            }
        }

        private static int AskForRuleSelection(string keyword, int max)
        {
            int choice = -1;

            bool invalidInput = true;
            while (invalidInput)
            {
                invalidInput = false;

                Console.WriteLine("Quelle regle voulez-vous " + keyword + "?");
                string input = Console.ReadLine();

                try
                {
                    choice = int.Parse(input);
                    invalidInput = choice < 1 || choice > max;
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

        private static Grammar GrammarModification(Grammar grammar)
        {
            bool invalidInput = true, done = false;
            while (invalidInput || !done)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                invalidInput = false;
                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu de l'application
                Console.WriteLine("1- Modifier une regle");
                Console.WriteLine("2- Supprimer une regle");
                Console.WriteLine("3- Ajouter une ou plusieurs regles");
                Console.WriteLine("4- Quitter");

                string input = Console.ReadLine();
                int choice = -1;
                switch (input)
                {
                    case "1":                                                               // Si 1 : Modification d'une regle
                        DisplayGrammarWithOptions(grammar);
                        choice = AskForRuleSelection("modifier", grammar.rules.Count);
                        ModifyRule(choice, grammar);
                        break;
                    case "2":                                                               // Si 2 : Suppression d'une regle
                        DisplayGrammarWithOptions(grammar);
                        choice = AskForRuleSelection("supprimer", grammar.rules.Count);

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
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Entrée invalide");
                        invalidInput = true;
                        break;
                }
            }

            return grammar;
        }

        private static bool ThereIsAnotherSRule(List<string> rules)
        {
            foreach(string rule in rules)
            {
                if (rule.Contains("S"))
                {
                    return true;
                }
            }
            return false;
        }

        private static void ModifyRule(int choice, Grammar grammar)
        {
            string newRule = AskRule(grammar.rules);
            List<string> beforeRules = grammar.rules.ToList();

            string beingReplacedRule = grammar.rules[choice - 1];
            grammar.rules[choice - 1] = newRule;

            if (newRule.Contains("S") || !beingReplacedRule.Contains("S") || ThereIsAnotherSRule(grammar.rules))
            {
                if (AskForConfirmation())
                {
                    WriteGrammarInFile(grammar.path, grammar.rules);
                }
            }
            else
            {
                Console.WriteLine("Vous ne pouvez pas retirer la seule règle S");
                Console.WriteLine("Aucun changement");
            }

        }

        private static List<string> RemoveRule(int choice, List<string> rules)
        {
            rules.RemoveAt(choice - 1);
            return rules;
        }
        private static bool RuleDuplicate(string rule, List<string> rules)
        {
            return rules.Contains(rule);
        }
        private static string AskRule(List<string> rules)
        {
            string rule = "";

            bool validRule = false;
            while (!validRule)
            {
                Console.WriteLine("Entrez une règle à la grammaire. Respectez le format suivant: S->e OU X->1 OU X->1X");
                rule = Console.ReadLine().Trim();

                if (Regex.IsMatch(rule, "^(?:[A-Z]->[0-1]{1}[A-Z]{1}|[A-Z]->[0-1]{1}|S->e)"))
                {
                    if (!RuleDuplicate(rule, rules))
                    {
                        validRule = true;
                    }
                }
                else
                {
                    Console.WriteLine("La règle n'est pas valide. Respectez le format suivant: S->e OU X->1 OU X->1X");
                }
            }

            return rule;
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
                string rule = AskRule(rules);

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
            return rules;
        }
    }
}
