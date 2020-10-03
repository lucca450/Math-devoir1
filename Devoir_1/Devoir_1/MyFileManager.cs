﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Devoir_1
{
    static class MyFileManager
    {
        public static string FormatPath(string fileName)
        {
            return string.Format(@".\{0}.txt", fileName);                       //  Formatage du chemin du fichier 
        }
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static void WriteGrammarInFile(Grammar grammar, List<string> addedRules = null)
        {
            StreamWriter sw = new StreamWriter(grammar.path, addedRules != null);

            if (addedRules!=null)
            {
                sw.WriteLine();
            }

            int i = 0;
            foreach (string rule in grammar.rules)                                          //  Boucle dans chaque règle
            {
                i++;
                if (i != grammar.rules.Count)
                    sw.WriteLine(rule);                                                     //  Écrit la règle dans le fichier
                else
                    sw.Write(rule);
            }

            sw.Close();
        }

        public static List<string> ReadRulesFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);

            List<string> rules = sr.ReadToEnd().Split("\r\n").ToList();

            sr.Close();

            return rules;
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
