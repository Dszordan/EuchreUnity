namespace Euchre.Models
{
    public class Team
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public int TeamNumber { get; private set; }
        public int Score { get; private set; }

        public Team(string player1Name, string player2Name, int teamNumber)
        {
            Player1 = new Player(this, player1Name, 1);
            Player2 = new Player(this, player2Name, 2);
            TeamNumber = teamNumber;
            Score = 0;
        }
        private Team() { }
        public void AddScore(int scoreToAdd)
        {
            Score += scoreToAdd;
        }
        public override bool Equals(object obj)
        {
            var teamToCompare = (Team)obj;
            return teamToCompare.TeamNumber == TeamNumber;
        }

        //public void WinRound()
        //{
        //    Score += 1;
        //}
        //public void WinRoundAllTricks()
        //{
        //    Score += 2;
        //}
        //public void WinAloneRound()
        //{
        //    Score += 4;
        //}
        //public Team(Player player1, Player player2)
        //{
        //    Player1 = player1;
        //    Player2 = player2;
        //}
    }
}
