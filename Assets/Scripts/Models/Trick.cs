
using System.Collections.Generic;
using System.Linq;
using Euchre.BusinessLogic;

namespace Euchre.Models
{
    public class Trick
    {
        private readonly Dictionary<Player, Card> _currentTrick;
        public Suit CurrentSuit { get; set; }

        public Trick()
        {
            _currentTrick = new Dictionary<Player, Card>();
            CurrentSuit = Suit.Undecided;
        }
        public void AddCard(Player player, Card cardPlayed)
        {
            _currentTrick.Add(player, cardPlayed);
        }
        public Card GetCardPlayed(Player player)
        {
            return _currentTrick[player];
        }
        public Player GetPlayerWhoPlayedCard(Card cardPlayed)
        {
            return
                _currentTrick.FirstOrDefault(
                    e => e.Value.CardNumber == cardPlayed.CardNumber
                        && e.Value.CardSuit == cardPlayed.CardSuit)
                        .Key;
        }
        public bool PlayerPlayed(Player player)
        {
            return _currentTrick.ContainsKey(player);
        }
    }
}
