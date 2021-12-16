using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using MemoryGame.Files;
using MemoryGame.Management;
using MemoryGame.Menus;
using Timer = System.Timers.Timer;

namespace MemoryGame.Cards
{
    public class CardManager
    {
        public List<Card> CardList { get; set; }
        public int cardSelected = 0;
        public int lastLocation;
        public static int nbMin = 0;

        #region Card Management

        public void GenerateCardList(IEnumerable<string> filesList)
        {
            var y = 1;
            var x = 0;
            CardList = new List<Card>();
            foreach (var card in filesList.Select(file => new Card(file, new Location(x, y))))
            {
                if (x + card.width + 28 >= Program.WindowWidth)
                {
                    x = 0;
                    y += 15;
                }
                else
                {
                    x += 33;
                }
                CardList.Add(card);
            }
            lastLocation = y+15;
        }

        public void DrawCardList(int i = 0)
        {
            if (i >= CardList.Count) return;
            var colors = ConsoleColor.Gray;
            if (i == cardSelected)
            {
                colors = ConsoleColor.Cyan;
            }
            CardList[i].Draw(colors);
            DrawCardList(i+1);
        }

        #endregion


        #region CardDisplay

        public void VerifCard(List<Card> plateau,int type = 0,Card cardOne=null, Card cardTwo=null)
        {
            switch (type)
            {
                case 0 when cardOne != null && cardTwo!=null && cardOne.path != cardTwo.path:
                    Thread.Sleep(2000);
                    cardOne.Display = false;
                    cardTwo.Display = false;
                    break;
                case 0:
                {
                    Console.SetCursorPosition(0, lastLocation + 1);
                    Console.Write("Il y a actuellement "+NbCardFind()+" paires de cartes découverte");
                    if (plateau.Count / 2 != NbCardFind()) return;
                    Menu.timer.StopTimer();
                    Result.EditResultGameOne((nbMin/2)+1);
                    Result.DisplayResultGame1();
                    break;
                }
                case 1 when cardOne != null && cardTwo!=null && cardOne.path != cardTwo.path:
                    Thread.Sleep(2000);
                    cardOne.Display = false;
                    cardTwo.Display = false;
                    break;
                case 1:
                {
                    if ((plateau.Count-Menu.falseCard) / 2 != NbCardFind()) return;
                    Menu.timer.StopTimer();
                    Result.EditResultGameTwo((nbMin/2)+1);
                    Result.DisplayResultGameTwo();
                    break;
                }
            }
        }

        public int NbCardFind(int type = 0)
        {
            var result = 0;
            switch (type)
            {
                case 0:
                    result = CardList.Count(card => card.Display) / 2;
                    break;
                case 1:
                    result = (CardList.Count(card => card.Display)) / 2;
                    break;
            }
            return result;
        }
        
        public int NbCardReturned(int type = 0)
        {
            int result;
            if (type == 0)
            {
                result = CardList.Count(card => card.Display);
            }else
            {
                result = CardList.Count(card => card.Display);
            }
            return result;
        }

        public void DisplayFalse()
        {
            foreach (var card in CardList)
            {
                card.Display = false;
            }
        }

        #endregion
    }
}