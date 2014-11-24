using System.Collections.Generic;
namespace Euchre.Models
{
    public class Deck
    {
        public List<Card> CurrentDeckCards { get; private set; }

        public Deck()
        {
            CurrentDeckCards = new List<Card>();
        }
        public void SetDeck(List<Card> newCardList)
        {
            CurrentDeckCards = newCardList;
        }
    }
}
