using System;
using System.Collections.Generic;
using MemoryGame.Cards;
using MemoryGame.Files;

namespace MemoryGame.Menus
{
    public struct MenuItem
    {
        public string name;
        public string description;
    }
    
    public class Menu
    {
        private static void DisplayCenterMenu(int nbItem)
        {
            const string title = "MemoryGames";
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
                var end = start + size;
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
                
            var footerWidth = ((Program.WindowWidth)/2)-footer.Length/2;
            if (start%2 != 0)
            {
                footerWidth++;
            }
            Console.SetCursorPosition(footerWidth,startH+hauteur-1);
            Console.WriteLine(footer);
        }

        public static void DisplayMenu(List<MenuItem> menu, int indexMenu = 0)
        {
            var nbItem = menu.Count;
            DisplayCenterMenu(nbItem);
            while (true)
            {
                var size = Convert.ToInt32(Program.WindowWidth * 0.25);
                var start = (Program.WindowWidth - size) / 2;

                var hauteur = nbItem + 7;
                var startH = (Program.WindowHeight - hauteur) / 2;

                int[] position = { start + 5, startH + 4 };
                for (var i = 0; i < menu.Count; i++)
                {
                    if (i == indexMenu)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(position[0] - 2, position[1]);
                        Console.Write(">");
                    }
                    else
                    {
                        Console.SetCursorPosition(position[0] - 2, position[1]);
                        Console.Write(" ");
                    }

                    Console.SetCursorPosition(position[0], position[1]);
                    Console.Write(menu[i].name);
                    position[1]++;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (indexMenu < menu.Count - 1)
                        {
                            indexMenu++;
                        }
                        continue;
                    case ConsoleKey.UpArrow:
                        if (indexMenu > 0)
                        {
                            indexMenu--;
                        }
                        continue;
                    case ConsoleKey.Enter:
                        StartFunction(indexMenu);
                        break;
                    default:
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        continue;
                }

                break;
            }
        }

        private static void StartFunction(int menuSelected)
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

        private static void LeaveGame()
        {
            while (true)
            {
                var msg = new Stack<string>();
                msg.Push("Merci d'avoir joué au MemoryGame");
                msg.Push("N'hésitez pas a revenir jouer");
                msg.Push("Il y a souvent des mises a jours");
                msg.Push(" ");
                msg.Push("Appuyez sur Entrée pour quitter");
                var nbItem = msg.Count;
                var hauteur = nbItem + 7;
                var startH = (Program.WindowHeight - hauteur) / 2;
                var size = Convert.ToInt32(Program.WindowWidth * 0.25);
                var start = (Program.WindowWidth - size) / 2;
                DisplayCenterMenu(nbItem);

                for (var i = msg.Count - 1; i >= 0; i--)
                {
                    var message = msg.Pop();
                    var msgWidth = (Program.WindowWidth - message.Length) / 2;
                    if (start % 2 != 0) msgWidth++;
                    
                    Console.ForegroundColor = msg.Count == 4 ? ConsoleColor.Magenta : ConsoleColor.Gray;

                    Console.SetCursorPosition(msgWidth, startH + msg.Count + 4);
                    Console.WriteLine(message);
                }
                
                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.SetCursorPosition(0, 0);
                        Console.Write(" ");
                        continue;
                }
                break;
            }
        }

        private static void StartGameOne()
        {
            Console.Clear();
            var cardList = new CardManager();
            //cardList.GenerateCardList(FilesManager);
            
            Console.ReadKey();
        }

        private static void StartGameTwo()
        {
            Console.WriteLine("GAME 2");
            Console.ReadKey();
        }
        
        public static List<MenuItem> GeneretedStartMenu()
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