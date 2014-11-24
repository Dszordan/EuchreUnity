using System.Collections.Generic;
using Euchre.BusinessLogic;

namespace Euchre.Models
{
    public class EuchreGame
    {
        public Team Team1 { get; private set; }
        public Team Team2 { get; private set; }
        public Suit Trump { get; private set; }
        public Stack<Card> Kitty { get; private set; }
        public Trick CurrentTrick { get; private set; }
        public List<Player> PlayerPassesList { get; private set; }
        public Player CurrentPlayer { get; set; }
        private Player[] _playerOrder;

        public EuchreGame(string team1Player1, string team1Player2, string team2Player1, string team2Player2)
        {
            Team1 = new Team(team1Player1, team1Player2,1);
            Team2 = new Team(team2Player1, team2Player2,2);
            CurrentTrick = new Trick();
            PlayerPassesList = new List<Player>();
            //Assume player order. (This maybe should be in business logic)
            _playerOrder = new Player[] { Team1.Player1, Team2.Player1, Team1.Player2, Team2.Player2 };
        }
        public void SetKitty(Stack<Card> leftoverCards)
        {
            Kitty = leftoverCards;
        }
        public void SetTrump(Suit trumpSuit)
        {
            Trump = trumpSuit;
        }
        public void AddCardToTrick(Player player, Card card)
        {
            CurrentTrick.AddCard(player, card);
        }
        public void ClearTrick()
        {
            CurrentTrick = new Trick();
        }
        public void AddPlayerPass(Player player)
        {
            PlayerPassesList.Add(player);
        }
        public void ClearPlayerPasses()
        {
            PlayerPassesList = new List<Player>();
        }
        public override string ToString()
        {
            var returnString = "";
            returnString += "Team 1 Members: Player " + Team1.Player1.PlayerNumber + " : " + Team1.Player1.PlayerName + ", " ;
            returnString += "Player " + Team1.Player2.PlayerNumber + " : " + Team1.Player2.PlayerName + ", ";
            returnString += "Team 2 Members: Player " + Team2.Player1.PlayerNumber + " : " + Team2.Player1.PlayerName + ", ";
            returnString += "Player " + Team2.Player2.PlayerNumber + " : " + Team2.Player2.PlayerName + ", ";
            return returnString;
        }
    }
}
