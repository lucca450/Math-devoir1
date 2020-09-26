using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Devoir_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName, fileText, userInput;
             

            Console.Write("Entrez le contenu du fichier: ");
            fileText = Console.ReadLine();

            if (writeToFile(fileText))
            {
                Console.WriteLine("Enregistrement effectué.\nVoici ce que contient le fichier:");
                readFromFile();
            }
            else
            {
                Console.WriteLine("L'enregistrement ne s'est pas effectué.");
            }


            Console.Write("Voulez-vous renommer le fichier? (o ou n)");
            userInput = Console.ReadLine();
            if (userInput.Trim() == "o" || userInput.Trim() == "O")
            {
                Console.Write("Entrez le nom du fichier: ");
                fileName = Console.ReadLine();

                if(fileName.Trim() != "")
                {
                    if (renameFile(@".\Grammaire.txt", @".\" + fileName + ".txt"))
                    {
                        Console.WriteLine("Le fichier a changé de nom.");
                    }
                    else
                    {
                        Console.WriteLine("Une erreur est survenue.");
                    }
                }
                else
                {
                    Console.WriteLine("Une erreur est survenue.");
                }
            }
            else if(userInput.Trim() == "n" || userInput.Trim() == "N")
            {
                Console.WriteLine("Fin du programme.");
            }
            else
            {
                Console.WriteLine("Entrée non valide.");
            }

            

        }

        static bool writeToFile(string texte)
        {
          
            var path = @".\Grammaire.txt";
            

            //StreamWriter sw = new StreamWriter(path);     // overright le texte dans le fichier.
            StreamWriter sw = new StreamWriter(path, true); // écrit à la suite du texte déjà présent.


            List<string> liste;

            liste = texte.Split('/').ToList();

            foreach (var item in liste)
            {
                if (item.Trim() != "")
                {
                    sw.WriteLine(item.Trim());
                }
            }
            sw.Close();        
            

            return true;
        }


        static bool renameFile(string old, string recent)
        {
            if (File.Exists(old))
            {
                File.Copy(old, recent, true);
                File.Delete(old);
                return true;
            }
            return false;
        }

        static void readFromFile()
        {

            string readText = File.ReadAllText(@".\Grammaire.txt");
           
            Console.WriteLine("J'ai trouvé ceci: " + readText);

        }

    }
}
