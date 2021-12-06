using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MemoryGame.Menus;

namespace MemoryGame.Files
{
    public class Result
    {
        private static Dictionary<string, string> resultGO; 

        #region GameOne

        private static void InitResultGameOne()
        {
            resultGO = new Dictionary<string,string>();
            var sr = new StreamReader("Result/GameOne.txt");
            var line = sr.ReadLine();
            while(line != null)
            {
                var tab = line.Split(':');
                if (resultGO.ContainsKey(tab[0]))
                {
                    resultGO.Remove(tab[0]);
                }
                resultGO.Add(tab[0],tab[1]);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public static void EditResultGameOne(string key, string value)
        {
            string name;
            do
            {
                Console.WriteLine("Saisir votre prénom, compris en 1 et 10 caractéres");
                name = Console.ReadLine();
            } while (name == null || name.Length > 10 || name.Length <= 0);
            var myresultGo = new Dictionary<string,string>();
            var sr = new StreamReader("Result/GameOne.txt");
            var line = sr.ReadLine();
            while(line != null)
            {
                var tab = line.Split(':');
                if (myresultGo.ContainsKey(tab[0]))
                {
                    myresultGo.Remove(tab[0]);
                }
                myresultGo.Add(tab[0],tab[1]);
                line = sr.ReadLine();
            }

            var t = false;
            foreach (var keyValuePair in myresultGo.Where(keyValuePair => keyValuePair.Key == key))
            {
                myresultGo.Remove(key);
                myresultGo.Add(key,value+" ("+name+")");
                t = true;
            }
            if(t) myresultGo.Add(key,value+" ("+name+")");
            
            sr.Close();
        }

        
        public static void DisplayResult()
        {
            InitResultGameOne();
            Console.Clear();
            var nbItem = resultGO.Count;
            DisplayCenterResult(nbItem);
            while (true)
            {
                var size = Convert.ToInt32(Program.WindowWidth * 0.25);
                var start = (Program.WindowWidth - size) / 2;

                var hauteur = nbItem + 7;
                var startH = (Program.WindowHeight - hauteur) / 2;

                int[] position = { start + 5, startH + 4 };
                foreach (var keyValuePair in resultGO)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(position[0], position[1]);
                    Console.Write(keyValuePair.Key + " : "+keyValuePair.Value);
                    position[1]++;
                }

                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Menu.DisplayMenu(Program.start);
                        break;
                    default:
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        continue;
                }

                break;
            }
        }
        
        
        
        
        
        
        
        
        private static void DisplayCenterResult(int nbItem)
        {
            const string title = "MemoryGames";
            const string title1 = "Score Jeu 1";
            const string footer = "Projet créer par Romain Pathé";
            var size = Convert.ToInt32(Program.WindowWidth*0.25);
            var start = (Program.WindowWidth - size) / 2;
            
            var hauteur = nbItem+7;
            var startH = ((Program.WindowHeight - hauteur) / 2);
            
            for (var i = start; i < start+size;i++)
            {
                if (i%2 != 0)
                {
                    Console.SetCursorPosition(i,startH);
                    Console.Write("*");
                    Console.SetCursorPosition(i, startH + hauteur);
                    Console.Write("*");
                }
            }

            if (start % 2 == 0)
            {
                start++;
            }
            for (var j = startH; j <= startH + hauteur; j++)
            {
                // TODO: Check si sur pc portable j'ai toujours autant de hauteurs sur les étoiles
                var end = start + size -1;
                Console.SetCursorPosition(start, j);
                Console.Write("*");
                Console.SetCursorPosition(end, j);
                Console.Write("*");
            }

            var titleWidth = ((Program.WindowWidth)/2)-title.Length/2;
            if (start%2 != 0)
            {
                titleWidth++;
            }
            Console.SetCursorPosition(titleWidth,startH+1);
            Console.WriteLine(title);
            
            var title1Width = ((Program.WindowWidth)/2)-title1.Length/2;
            if (start%2 != 0)
            {
                title1Width++;
            }
            Console.SetCursorPosition(title1Width,startH+2);
            Console.WriteLine(title1);
                
            var footerWidth = ((Program.WindowWidth)/2)-footer.Length/2;
            if (start%2 != 0)
            {
                footerWidth++;
            }
            Console.SetCursorPosition(footerWidth,startH+hauteur-1);
            Console.WriteLine(footer);
        }
        
        #endregion

    }
}