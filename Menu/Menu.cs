using System;
using System.Collections.Generic;

namespace MemoryGame.Menu
{
    public class Menu
    {
        
        public struct MenuItem
        {
            public string name;
            public string description;
        }
        
        public static void DisplayCenterMenu(int nbItem)
        {
            const string title = "MemoryGames";
            const string footer = "Projet créer par Romain Pathé";
            var size = Convert.ToInt32(Program.WindowWidth*0.25);
            var start = (Program.WindowWidth - size) / 2;
            
            var hauteur = nbItem+7;
            var starth = (Program.WindowHeight - hauteur) / 2;
            
            for (int i = start; i < start+size;i++)
            {
                if (i%2 != 0)
                {
                    Console.SetCursorPosition(i,starth);
                    Console.Write("*");
                    Console.SetCursorPosition(i, starth + hauteur);
                    Console.Write("*");
                }
            }

            for (int j = starth; j <= starth + hauteur; j++)
            {
                int end = start + size;
                if (start % 2 == 0)
                {
                    start++;
                }
                if (end % 2 == 0)
                {
                    end--;
                }
                Console.SetCursorPosition(start, j);
                Console.Write("*");
                Console.SetCursorPosition(end, j);
                Console.Write("*");
            }

            var titleWidth = (Program.WindowWidth- title.Length)/2;
            if (start%2 != 0)
            {
                titleWidth++;
            }
            Console.SetCursorPosition(titleWidth,starth+1);
            Console.WriteLine(title);
                
            var footerWidth = (Program.WindowWidth - footer.Length)/2;
            if (start%2 != 0)
            {
                footerWidth++;
            }
            Console.SetCursorPosition(footerWidth,starth+hauteur-1);
            Console.WriteLine(footer);
        }
        
                public static void DisplayMenu(List<MenuItem> menu,int menuSelected = 0)
        {
            int nbItem = menu.Count;
            DisplayCenterMenu(nbItem);
            var size = Convert.ToInt32(Program.WindowWidth*0.25);
            var start = (Program.WindowWidth - size) / 2;
            
            var hauteur = nbItem+7;
            var starth = (Program.WindowHeight - hauteur) / 2;

                int[] position = {start+5,starth+4};
                for (int i = 0; i < menu.Count;i++)
                {
                    
                    
                    if (i == menuSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(position[0]-2,position[1]);
                        Console.Write(">");
                    }
                    else
                    {
                        Console.SetCursorPosition(position[0]-2,position[1]);
                        Console.Write(" ");
                    }
                    
                    Console.SetCursorPosition(position[0],position[1]);
                    Console.Write(menu[i].name);
                    position[1]++;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.SetCursorPosition(0,0);
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (menuSelected < menu.Count-1)
                        {
                            menuSelected++;
                        }
                        DisplayMenu(menu,menuSelected);
                        break;
                    case ConsoleKey.UpArrow:
                        if (menuSelected > 0)
                        {
                            menuSelected--;
                        }
                        DisplayMenu(menu,menuSelected);
                        break;
                    case ConsoleKey.Enter:
                        StartFunction(menuSelected);
                        break;
                    default:
                        Console.SetCursorPosition(0,0);
                        Console.Write(" ");
                        DisplayMenu(menu,menuSelected);
                        break;
                }

        }

        public static void StartFunction(int menuSelected)
        {
            if (menuSelected == 0)
            {
                StartGameOne();
            }
            else if(menuSelected == 1)
            {
                StartGameTwo();
            }
            else
            {
                Console.Clear();
                LeaveGame();
            }
        }

        public static void LeaveGame()
        {
            var msg = new Stack<string>();
            msg.Push("Merci d'avoir joué au MemoryGame");
            msg.Push("N'hésitez pas a revenir jouer");
            msg.Push("Il y a souvent des mises a jours");
            msg.Push(" ");
            msg.Push("Appuyez sur Entrée pour quitter");
            var nbItem = msg.Count;
            var hauteur = nbItem+7;
            var starth = (Program.WindowHeight - hauteur) / 2;
            var size = Convert.ToInt32(Program.WindowWidth*0.25);
            var start = (Program.WindowWidth - size) / 2;
            DisplayCenterMenu(nbItem);

            for (var i = msg.Count-1; i >= 0;i--)
            {
                var message = msg.Pop();
                var msgWidth = (Program.WindowWidth- message.Length)/2;
                if (start%2 != 0)
                {
                    msgWidth++;
                }

                if (msg.Count == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.SetCursorPosition(msgWidth,starth+msg.Count+4);
                Console.WriteLine(message);
            }
            
            
            
            Console.SetCursorPosition(0,0);
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    break;
                default:
                    Console.SetCursorPosition(0,0);
                    Console.Write(" ");
                    LeaveGame();
                    break;
            }

        }

        public static void StartGameOne()
        {
            Console.WriteLine("GAME 1");
            Console.ReadKey();
        }

        public static void StartGameTwo()
        {
            Console.WriteLine("GAME 2");
            Console.ReadKey();
        }
        
        public static List<MenuItem> GeneretedMenu()
        {
            List<MenuItem> menu = new List<MenuItem>();
            MenuItem item = new MenuItem();
            item.name = "Jeu 1";
            menu.Add(item);
            
            MenuItem item2 = new MenuItem();
            item2.name = "Jeu 2";
            menu.Add(item2);
            
            MenuItem item3 = new MenuItem();
            item3.name = "Quitter le projet";
            menu.Add(item3);
            
            return menu;
        }
        
        
    }
}