using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Devoir_1
{
    class Grammar
    {
        public List<string> rules = new List<string>();
        public string fileName = "";
        public string path = "";

        public Grammar(string fileName, string path, List<string> rules)
        {
            this.fileName = fileName;
            this.path = path;
            this.rules = rules;
        }

        public bool RuleDuplicate()                                             // Retourne vrai si une règle est double
        {
            return rules.Count != rules.Distinct().Count();
        }
        public Grammar(bool isImport = false)
        {
            string fileName, path;
            List<string> rules;

            if (!isImport)                                              //  Si on veut créer une grammaire
            {
                fileName = MyConsole.AskFileName();                             //  Demande du nom du fichier
                path = MyFileManager.FormatPath(fileName);                      //  Formatage du chemin du fichier

                rules = MyConsole.AskRules(this);                               //  Demander les règles à l'utilisateur

                if (MyConsole.AskForConfirmation())                             //  Demande de sauvegarder
                {
                    this.fileName = fileName;
                    this.path = path;
                    this.rules = rules;
                    MyFileManager.WriteGrammarInFile(this);                     //  Écriture de la règle dans le fichier
                }
            }       
            else
            {                                                           //  Si on veut importer une grammaire
                this.fileName = MyConsole.AskFileName(true);                //  Demande du nom du fichier
                this.path = MyFileManager.FormatPath(this.fileName);        //  Formatage du chemin du fichier

                this.rules = MyFileManager.ReadRulesFromFile(this.path);    //  Lecture des règles du fichier

                if (VerifyRules())                                      //  Si règles valides
                {
                    MyConsole.SuccessfulImport();                           //  Réussite importation
                }else
                {                                                       // Si règle(s) invalide(s)
                    MyConsole.InvalidImport();                          // Roll Back
                    this.fileName = null;
                    this.path = null;
                    this.rules = null;
                }
            }
        }

        public bool VerifyRules()
        {
            bool error = false;
            int sRuleCounter = 0;
            foreach (var rule in rules)                                 //  Pour chaque règle
            {
                if (rule.Contains("S"))                                     // Si   la règle contient S
                {
                    sRuleCounter++;                                             //  conteur de règle S
                }
                if (!Regex.IsMatch(rule, "^(?:[A-Y]->[0-1]{1}[A-Y]{1}|[A-Y]->[0-1]{1}|S->e)"))  //   Si le format est valide
                {
                    error = true;
                }
            }

            if (RuleDuplicate())                                        //  S'il existe des doublons
            {
                error = true;
            }

            return !error && sRuleCounter >= 1;
        }

        public void Edit()
        {
            bool done = false;
            while (!done)                                                               //  Tant que pas fini
            {
                int choice;
                string input = MyConsole.AskForMenuSelection();                             //  Demande ce que l'utilisateur veut faire                                
                switch (input)
                {
                    case "1":                                                               //  Modifier une règle
                        MyConsole.DisplayGrammarWithOptions(this);
                        choice = MyConsole.AskForRuleSelection("modifier", rules.Count);        //  Demander quelle règle modifier
                        ModifyRule(choice);                                                     //  Modifier la règle
                        break;
                    case "2":                                                               //  Supprimer une règle
                        if(rules.Count != 0)
                        {
                            MyConsole.DisplayGrammarWithOptions(this);
                            choice = MyConsole.AskForRuleSelection("supprimer", rules.Count);   //  Demander quelle règle supprimer
                            RemoveRule(choice);                                                 //  Supprimer la règle
                        }
                        else
                            MyConsole.NoRuleToDelete();
                        break;
                    case "3":                                                               //  Ajouter des règles
                        List<string> newRules = MyConsole.AskRules(this, true);                 //  Demande les règles

                        if (MyConsole.AskForConfirmation())                                     //  Confirmer la sauvegarde
                        {
                            rules.AddRange(newRules);                                           //  Ajouter les règles à l'objet grammaire
                            MyFileManager.WriteGrammarInFile(this, newRules);                   //  Ajouter les règles au fichier de la grammaire
                        }
                        break;
                    case "4":                                                               //  Quitter
                        done = true;
                        break;
                    default:
                        MyConsole.Error();
                        break;
                }
            }
        }
        private void ModifyRule(int choice)
        {
            string newRule = MyConsole.AskRule(this);                                       //  Demander une règle
            List<string> beforeRules = rules.ToList();                                      //  règles avant modification

            string beingReplacedRule = rules[choice - 1];                                   //  règles qui sera remplacé
            rules[choice - 1] = newRule;                                                    //  Remplacement de la règle

            if (newRule.Contains("S") || !beingReplacedRule.Contains("S") || ThereIsAnotherSRule())     //  Si la nouvelle règle contient S ou la règle remplacée ne contient pas de S ou Si il y a une autre règle S
            {
                if (MyConsole.AskForConfirmation())                                         //  Demande de sauvegarde
                {
                    MyFileManager.WriteGrammarInFile(this);                                 //  Mise à jour du fichier
                }
                else
                {
                    rules = beforeRules;                                                    //  Rollback avec le backup
                    MyConsole.NoChange();
                }
            }
            else
            {                                                                               //  Si aucune autre règle S existe
                MyConsole.YouCantChangeTheOnlySRule();                                      //  rollback avec le backup
                rules = beforeRules;
                MyConsole.NoChange();
            }
        }
        private bool ThereIsAnotherSRule()
        {
            foreach (string rule in rules)                                                  //  Pour chaque règle
            {
                if (rule.Contains("S"))                                                         // Si elle contient S
                {
                    return true;
                }
            }
            return false;
        }
        private void RemoveRule(int choice)
        {
            List<string> beforeRules = rules.ToList();                                      //  backup
            rules.RemoveAt(choice - 1);                                                     //  Retrait de la règle

            if (!ThereIsAnotherSRule())                                                     //  Si il n'y a pas d'autre règle S
            {
                MyConsole.YouCantChangeTheOnlySRule();
                MyConsole.NoChange();
                rules = beforeRules;                                                        //  Rollback avec le backup
            }
        }
        public void Visualize()
        {
            bool done = false;
            while(!done)
            {
                MyConsole.DisplayGrammar(this);

                string input = MyConsole.GrammarMenu();
                switch (input)
                {
                    case "1":                                                               // Si 1 : Modification de la grammaire
                        Edit();
                        break;
                    case "2":                                                               // Si 2 : Suppression de la grammaire
                        Delete();
                        done = true;
                        break;
                    case "3":                                                               // Si 3 : Affichage de la grammaire
                        MyConsole.DisplayGrammar(this);
                        break;
                    case "4":                                                               // Si 4 : Quitter
                        done = true;
                        break;
                    default:
                        MyConsole.Error();
                        break;
                }
            }  
        }

        private void Delete()
        {
            MyFileManager.DeleteFile(path);                                                 //  Suppression du fichier
            path = null;                                                                    //  Remise de l'objet grammaire à null
            fileName = null;
            rules = null;
        }
    }
}
