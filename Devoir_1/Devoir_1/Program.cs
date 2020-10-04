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


        static void Main(string[] args)
        {
            Grammar grammar = null;
            bool done = false;
            while (!done)
            {
                string input;
                if (grammar == null)
                    input = MyConsole.MainMenu();
                else
                    input = MyConsole.MainMenu(grammar.fileName);

                switch (input)
                {
                    case "1":                                                               // Si 1 : Création de la grammaire
                        grammar = new Grammar();
                        break;
                    case "2":                                                               // Si 2 : Importation de la grammaire
                        if (grammar == null)
                            grammar = new Grammar(true);
                        else
                            if (MyConsole.AskToRechargeGrammar())
                                grammar = new Grammar(true);
                        if (grammar.rules == null)
                        {
                            grammar = null;
                            MyConsole.WaitForAnyInput();
                        }
                        break;
                    case "3":                                                               // Si 3 : Visualisation de la grammaire
                        if (grammar != null)
                            grammar.Visualize();
                        else
                        {
                            MyConsole.MustImportGrammarFirst();
                            MyConsole.WaitForAnyInput();
                        }
                        break;
                    case "4":                                                               // Si 4 : Exécution de l'automate
                        if(grammar != null)
                        {
                            Robot automaton = new Robot(grammar);
                            automaton.Execute();
                        }
                        else
                        {
                            MyConsole.MustImportGrammarFirst();
                            MyConsole.WaitForAnyInput();
                        }
                        break;
                    case "5":                                                               // Si 5 : Fermeture de l'application
                        done = true;
                        break;
                    default:                                                                // Si entrée invalide
                        MyConsole.Error();
                        break;
                }
                
            }



            
        }

        
 

        

        
    }
}
