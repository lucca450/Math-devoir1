using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        public bool RuleDuplicate()
        {
            return rules.Count != rules.Distinct().Count();
        }
        public Grammar(bool isImport = false)
        {
            string fileName, path;
            List<string> rules;

            if (!isImport)
            {
                fileName = MyConsole.AskFileName();                             //  Lecture du nom du fichier
                path = MyFileManager.FormatPath(fileName);                      //  Formatage du chemin du fichier

                rules = MyConsole.AskRules(this);

                if (MyConsole.AskForConfirmation())
                {
                    this.fileName = fileName;
                    this.path = path;
                    this.rules = rules;
                    MyFileManager.WriteGrammarInFile(this);
                }
            }
            else
            {
                this.fileName = MyConsole.AskFileName(true);
                this.path = MyFileManager.FormatPath(this.fileName);

                this.rules = MyFileManager.ReadRulesFromFile(this.path);

                if (VerifyRules())
                {
                    MyConsole.SuccessfulImport();
                }else
                {
                    MyConsole.InvalidImport();
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
            foreach (var rule in rules)
            {
                if (rule.Contains("S"))
                {
                    sRuleCounter++;
                }
                if (!Regex.IsMatch(rule, "^(?:[A-Z]->[0-1]{1}[A-Z]{1}|[A-Z]->[0-1]{1}|S->e)") || RuleDuplicate()) 
                {
                    error = true;
                }
            }

            return !error && sRuleCounter >= 1;
        }

        public void Edit()
        {
            bool done = false;
            while (!done)
            {
                int choice;
                string input = MyConsole.AskForMenuSelection();
                switch (input)
                {
                    case "1":
                        MyConsole.DisplayGrammarWithOptions(this);
                        choice = MyConsole.AskForRuleSelection("modifier", rules.Count);
                        ModifyRule(choice);
                        break;
                    case "2":
                        MyConsole.DisplayGrammarWithOptions(this);
                        choice = MyConsole.AskForRuleSelection("supprimer", rules.Count);
                        RemoveRule(choice);
                        break;
                    case "3":
                        List<string> newRules = MyConsole.AskRules(this, true);

                        if (MyConsole.AskForConfirmation())
                        {
                            rules.AddRange(newRules);
                            MyFileManager.WriteGrammarInFile(this, newRules);
                        }
                        break;
                    case "4":
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
            string newRule = MyConsole.AskRule(this);
            List<string> beforeRules = rules.ToList();

            string beingReplacedRule = rules[choice - 1];
            rules[choice - 1] = newRule;

            if (newRule.Contains("S") || !beingReplacedRule.Contains("S") || ThereIsAnotherSRule())
            {
                if (MyConsole.AskForConfirmation())
                {
                    MyFileManager.WriteGrammarInFile(this);
                }
                else
                {
                    rules = beforeRules;
                    MyConsole.NoChange();
                }
            }
            else
            {
                MyConsole.YouCantChangeTheOnlySRule();
                rules = beforeRules;
                MyConsole.NoChange();
            }
        }
        private bool ThereIsAnotherSRule()
        {
            foreach (string rule in rules)
            {
                if (rule.Contains("S"))
                {
                    return true;
                }
            }
            return false;
        }
        private void RemoveRule(int choice)
        {
            rules.RemoveAt(choice - 1);
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
            MyFileManager.DeleteFile(path);
            path = null;
            fileName = null;
            rules = null;
        }
    }
}
