using Euchre.BusinessLogic;

namespace Euchre.Models
{
    public class Card
    {
        public Suit CardSuit { get; private set; }
        public int CardNumber { get; private set; }
        public bool Trump { get; private set; }

        private Card() { }
        public Card(int cardNumber, Suit cardSuit)
        {
            CardSuit = cardSuit;
            CardNumber = cardNumber;
            Trump = false;
        }
        public Card(int cardNumber, Suit cardSuit, bool trump)
        {
            CardSuit = cardSuit;
            CardNumber = cardNumber;
            Trump = trump;
        }
        public void SetTrump()
        {
            Trump = true;
        }
        public void RemoveTrump()
        {
            Trump = false;
        }
        public override string ToString()
        {
            var returnString = string.Empty;
            returnString += CardNumber + ":" + CardSuit.ToString().Substring(0, 1);
            return returnString;
        }
    }
}
