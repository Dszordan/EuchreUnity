using System.Collections.Generic;
using System.Linq;

namespace Euchre.Models
{
    public class Hand
    {
        public List<Card> CardList { get; private set; }

        public Hand()
        {
            CardList = new List<Card>();
        }
        public void AddCard(Card card)
        {
            CardList.Add(card);
        }
        public void RemoveCard(Card card)
        {
            CardList.Remove(card);
        }
        public void ClearHand()
        {
            CardList = new List<Card>();
        }
        public override string ToString()
        {
            string returnString = string.Empty;
            var cardString = CardList.Select(card => card.ToString()).Aggregate((i, j) => i + "," + j);
            returnString += "Cards in hand: " + cardString;
            return returnString;
        }
    }
}
