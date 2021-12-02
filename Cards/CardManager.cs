using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MemoryGame.Cards
{
    public class CardManager
    {
        public List<Card> CardList { get; set; }
        public int cardSelected = 0;
        public int lastLocation = 0;

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

        public void VerifCard(Card cardOne, Card cardTwo)
        {
            if (cardOne.path != cardTwo.path)
            {
                Thread.Sleep(2000);
                cardOne.Display = false;
                cardTwo.Display = false;
            }
            else
            {
                Console.SetCursorPosition(0, lastLocation + 1);
                Console.Write("Il y a actuellement "+NbCardFind()+" paires de cartes découverte");
                // TODO : Faire si deux carte son identique

            }
        }

        public int NbCardFind()
        {
            return CardList.Count(card => card.Display)/2;
        }
        
        public int NbCardReturned()
        {
            return CardList.Count(card => card.Display);
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