using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Devoir_1
{
    class Program
    {
        static void CreateGrammar()
        {
            bool fileExists = true;
            string path = "";
            while (fileExists)
            {
                Console.WriteLine("Entrez le nom du fichier: ");
                string fileName = Console.ReadLine().Trim();                        //  Lecture du nom du fichier

                path = string.Format(@".\{0}.txt", fileName);                       //  Formatage du chemin du fichier

                fileExists = File.Exists(path);                                     //  Vérification de l'existance du fichier

                if (fileExists)
                    Console.WriteLine("Une grammaire avec ce nom existe déjà.\n");
            }

            StreamWriter sw = new StreamWriter(path,true);                         //  Écrit à la suite du texte déjà présent.

            bool validInput;
            bool done = false;
            while (!done)                                                           //  Tant que l'utilisateur n'a pas fini d'écrire de règle
            {
                Console.WriteLine("Entrer une règle à la grammaire");
                string rule = Console.ReadLine().Trim();                                //  Lecture de la règle

                sw.WriteLine(rule);                                                     //  Écriture de la règle dans le fichier

                Console.WriteLine(string.Format("Règle ajoutée: {0}", rule));

                validInput = false;
                while (!validInput)                                                     //  Tant que l'utiliseur n'a pas de entrée de donnée valide
                {
                    validInput = true;

                    Console.WriteLine("Voulez-vous ajouter une autre règle? o/n");
                    string input = Console.ReadLine();                                      //  Entrée de l'utilisateur
                    switch (input)
                    {
                        case "o":                                                           //  Si oui
                            break;
                        case "n":                                                           //  Si non
                            done = true;
                            break;
                        default:                                                            //  Si entrée invalide
                            Console.WriteLine("Entrée invalide");
                            validInput = false;
                            break;
                    }
                }
            }

            validInput = false;
            while (!validInput)                                                         //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                validInput = true;

                Console.WriteLine("Voulez-vous sauvegarder la grammaire? o/n");             //  Demande de sauvegarde de la grammaire
                string input = Console.ReadLine();                                          //  Lecture de la réponse

                switch (input)
                {
                    case "o":                                                               //  Si oui
                        break;
                    case "n":                                                               //  Si non
                        sw.Close();
                        File.Delete(path);
                        break;
                    default:                                                                //  Si entrée invalide
                        Console.WriteLine("Entrée invalide");
                        validInput = false;
                        break;
                }
            }

            sw.Close();
        }
        static void ImportGrammar()
        {

        }

        static void Main(string[] args)
        {
            bool invalidInput = true;

            while (invalidInput)                                                        //  Tant que l'utiliseur n'a pas de entrée de donnée valide
            {
                Console.WriteLine("Choisissez parmis les fonctions suivantes :");       //  Menu de l'application
                Console.WriteLine("1- Créer une nouvelle grammaire");
                Console.WriteLine("2- Charger une grammaire existante");
                Console.WriteLine("3- Quitter");

                string input = Console.ReadLine();                                      //  Lecture de la réponse

                switch(input)
                {
                    case "1":                                                               // Si 1 : Création de la grammaire
                        CreateGrammar();
                        break;
                    case "2":                                                               // Si 2 : Importation de la grammaire
                        ImportGrammar();
                        break;
                    case "3":                                                               // Si 3 : Fermeture de l'application
                        invalidInput = false;
                        break;
                    default:                                                                // Si entrée invalide
                        Console.WriteLine("Entrée invalide");
                        break;
                }
            }
        }
    }
}
