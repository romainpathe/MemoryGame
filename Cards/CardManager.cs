using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Cards
{
    public class CardManager
    {
        public List<Card> CardList { get; set; }


        #region Card Management

        public void GenerateCardList(IEnumerable<string> filesList)
        {
            var y = 0;
            CardList = new List<Card>();
            foreach (var card in filesList.Select(file => new Card(file, new Location(0, y))))
            {
                CardList.Add(card);
                y += 20;
            }
        }

        public void DrawCardList()
        {
            foreach (var card in CardList)
            {
                card.Draw();
            }
        }

        #endregion
        
    }
}