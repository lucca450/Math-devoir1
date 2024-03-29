﻿using System;

namespace Devoir_1
{
    class ConsoleTable
    {
        private readonly int tableWidth;

        public ConsoleTable(int tableWidth)
        {
            this.tableWidth = tableWidth;
        }
        //Référence sur le site suivant: https://stackoverflow.com/questions/856845/how-to-best-way-to-draw-table-in-console-app-c
        //Ces fonctions nous permettent d'avoir un tableau plus beau visuellement.
        public void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        public void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        public string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
