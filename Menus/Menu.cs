using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using MemoryGame.Cards;
using MemoryGame.Files;
using Timer = MemoryGame.Management.Timer;

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
            const string footer = "Projet créé par Romain Pathé";
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
            switch (menuSelected)
            {
                case 0:
                    StartGameOne();
                    break;
                case 1:
                    StartGameTwo();
                    break;
                default:
                    Console.Clear();
                    LeaveGame();
                    break;
            }
        }

        private static void LeaveGame()
        {
            while (true)
            {
                var msg = new Stack<string>();
                msg.Push("Merci d'avoir joué au MemoryGame");
                msg.Push("N'hésitez pas a revenir jouer");
                msg.Push("Il y a souvent des mises à jour");
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
            }
        }

        public static Timer timer;
        private static void StartGameOne()
        {
            Console.Clear();
            FilesManager.InitFilesList();
            var files = new FilesManager();
            
            
            // TODO: SelectedCard
            var statusFilesSelected = files.GenerateFilesSelected(2);
            if (statusFilesSelected.IsError)
            {
                Console.WriteLine("Erreur, merci de relancer le programme");
            }
            else
            {
                var cardList = new CardManager();
                cardList.GenerateCardList(files.FilesListSelected);
                bool end = false;
                int lastSelect = -1;
                cardList.DrawCardList();
                Thread.Sleep(5000);
                cardList.DisplayFalse();
                timer = new Timer();
                CardManager.nbMin = 0;
                while (!end)
                {
                    cardList.DrawCardList();
                    var key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (cardList.cardSelected == 0)
                            {
                                cardList.cardSelected = cardList.CardList.Count - 1;
                            }
                            else
                            {
                                cardList.cardSelected--;
                            }

                            break;
                        case ConsoleKey.RightArrow:
                            if (cardList.cardSelected == cardList.CardList.Count - 1)
                            {
                                cardList.cardSelected = 0;
                            }
                            else
                            {
                                cardList.cardSelected++;
                            }

                            break;
                        case ConsoleKey.Enter:
                            if (!cardList.CardList[cardList.cardSelected].Display)
                            {
                                if (cardList.NbCardReturned() % 2 != 0)
                                {
                                    cardList.CardList[cardList.cardSelected].Display = true;
                                    cardList.DrawCardList();
                                    cardList.VerifCard(cardList.CardList,0,cardList.CardList[lastSelect],cardList.CardList[cardList.cardSelected]);
                                }
                                else
                                {
                                    cardList.CardList[cardList.cardSelected].Display = true;
                                    lastSelect = cardList.cardSelected;
                                    cardList.VerifCard(cardList.CardList);
                                }
                                CardManager.nbMin++;
                            }
                            break;
                        case ConsoleKey.Escape:
                            end = true;
                            Console.Clear();
                            DisplayMenu(Program.start);
                            break;
                    }
                }
            }
        }

        public static int falseCard = 1;

        private static void StartGameTwo()
        {
            Console.Clear();
            FilesManager.InitFilesList();
            var files = new FilesManager();
            // TODO: SelectedCard
            int nbCardSelected = SelectCardNumber(10, FilesManager.FilesNumber-2,"Nombre de carte que vous souhaitez mettre :");
            switch (SelectCardNumber(1, 4,"Séléction de la difficulté :"))
            {
                case 1:
                    falseCard = nbCardSelected / 10;
                    break;
                case 2:
                    falseCard = nbCardSelected / 8;
                    break;
                case 3:
                    falseCard = nbCardSelected / 6;
                    break;
                case 4:
                    falseCard = nbCardSelected / 4;
                    break;
            }

            if (FilesManager.FilesNumber-nbCardSelected <= falseCard)
            {
                falseCard = 2;
            }
            var statusFilesSelected = files.GenerateFilesSelected(nbCardSelected+falseCard,1,falseCard);
            if (statusFilesSelected.IsError)
            {
                Console.WriteLine("Erreur, merci de relancer le programme");
            }
            else
            {
                var cardList = new CardManager();
                cardList.GenerateCardList(files.FilesListSelected);
                var end = false;
                var lastSelect = -1;
                cardList.DrawCardList();
                Thread.Sleep(5000);
                cardList.DisplayFalse();
                timer = new Timer();
                CardManager.nbMin = 0;
                while (!end)
                {
                    cardList.DrawCardList();
                    var key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (cardList.cardSelected == 0)
                            {
                                cardList.cardSelected = cardList.CardList.Count - 1;
                            }
                            else
                            {
                                cardList.cardSelected--;
                            }

                            break;
                        case ConsoleKey.RightArrow:
                            if (cardList.cardSelected == cardList.CardList.Count - 1)
                            {
                                cardList.cardSelected = 0;
                            }
                            else
                            {
                                cardList.cardSelected++;
                            }

                            break;
                        case ConsoleKey.Enter:
                            if (!cardList.CardList[cardList.cardSelected].Display)
                            {
                                if (cardList.NbCardReturned() % 2 != 0)
                                {
                                    cardList.CardList[cardList.cardSelected].Display = true;
                                    cardList.DrawCardList();
                                    cardList.VerifCard(cardList.CardList,1,cardList.CardList[lastSelect],cardList.CardList[cardList.cardSelected]);
                                }
                                else
                                {
                                    cardList.CardList[cardList.cardSelected].Display = true;
                                    lastSelect = cardList.cardSelected;
                                    cardList.VerifCard(cardList.CardList,1);
                                }
                                CardManager.nbMin++;
                            }
                            break;
                        case ConsoleKey.Escape:
                            end = true;
                            Console.Clear();
                            DisplayMenu(Program.start);
                            break;
                        default:
                            continue;
                    }
                }
            }
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


        public static int SelectCardNumber(int min, int max,string title)
        {
            int result;
            int nbItem = 3;
            do
            {
                Console.Clear();
                DisplayCenterMenu(nbItem);
                var size = Convert.ToInt32(Program.WindowWidth * 0.25);
                var start = (Program.WindowWidth - size) / 2;

                int hauteur = nbItem + 7;
                var startH = (Program.WindowHeight - hauteur) / 2;
                int[] position = { start + 5, startH + 4 };
                
                for (var i = 0; i < nbItem; i++)
                {
                    Console.SetCursorPosition(position[0], position[1]);
                    switch (i)
                    {
                        case 0:
                            Console.Write(title);
                            break;
                        case 1:
                            break;
                        case 2:
                            Console.Write("(Minimum: "+min+" | Maximum: "+max+")");
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Le nombre saisie n'est pas valide !");
                            break;
                    }
                    position[1]++;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.SetCursorPosition(position[0], position[1]-nbItem+1);
                result = Convert.ToInt32(Console.ReadLine());
                if (nbItem == 3) nbItem++;
            } while (result < min || result > max);
            Console.Clear();
            return result;
        }
        
    }
}