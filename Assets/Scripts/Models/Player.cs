namespace Euchre.Models
{
    public class Player : IPlayer
    {
        public string PlayerName { get; private set; }
        public int PlayerNumber { get; private set; }
        public Team PlayerTeam { get; private set; }
        public Hand CurrentHand { get; private set; }

        private Player() { }
        public Player(Team playerTeam, string playerName, int playerNumber)
        {
            PlayerName = playerName;
            PlayerTeam = playerTeam;
            PlayerNumber = playerNumber;
            CurrentHand = new Hand();
        }
        public void ReceiveCard(Card card)
        {
            CurrentHand.AddCard(card);
        }
        public void RemoveCard(Card card)
        {
            CurrentHand.RemoveCard(card);
        }
        public override string ToString()
        {
            var returnString = string.Empty;
            returnString += "Player info: Name " + PlayerName + " Number " + PlayerNumber + " Team " + PlayerTeam.TeamNumber;
            return returnString;
        }
        public override bool Equals(object obj)
        {
            var playerObjectToCompare = (IPlayer) obj;
            return playerObjectToCompare.PlayerNumber == PlayerNumber && playerObjectToCompare.PlayerTeam.Equals(PlayerTeam);
        }
    }

    public interface IPlayer
    {
        string PlayerName { get; }
        int PlayerNumber { get; }
        Team PlayerTeam { get; }
        Hand CurrentHand { get; }
        void ReceiveCard(Card card);
        void RemoveCard(Card card);
    }
}
