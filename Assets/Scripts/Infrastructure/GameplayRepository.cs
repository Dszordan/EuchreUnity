using System.Collections.Generic;
using Euchre.BusinessLogic;
using Euchre.Models;
namespace Euchre.Infrastructure
{
    public class GameplayRepositoryFactory
    {
        public static IGameplayRepository Create()
        {
            return new GameplayRepository();
        }
    }
    class GameplayRepository : IGameplayRepository
    {
        public EuchreGame GetCurrentEuchreGame()
        {
            return GameController.CurrentGame;
        }
        public void CreateTeams(string team1Player1, string team1Player2, string team2Player1, string team2Player2)
        {
            GameController.CurrentGame = new EuchreGame(team1Player1, team1Player2, team2Player1, team2Player2);
        }
        public void GivePlayerCard(Player playerReceivingCard, Card cardToReceive)
        {
            playerReceivingCard.ReceiveCard(cardToReceive);
        }
        public void RemovePlayerCard(Player playerRemovingCard, Card cardToRemove)
        {
            playerRemovingCard.RemoveCard(cardToRemove);
        }
        public void AddCardToTrick(Player player, Card card)
        {
            var currentGame = GetCurrentEuchreGame();
            currentGame.AddCardToTrick(player, card);
        }
        public void SetTrickSuit(Suit trickSuit)
        {
            GetCurrentEuchreGame().CurrentTrick.CurrentSuit = trickSuit;
        }
        public void ClearTrick()
        {
            var currentGame = GetCurrentEuchreGame();
            currentGame.ClearTrick();
        }
        public List<Player> GetPlayerPasses()
        {
            return GetCurrentEuchreGame().PlayerPassesList;
        }
        public void PlayerPass(Player player)
        {
            GetCurrentEuchreGame().AddPlayerPass(player);
        }
        public Player GetCurrentPlayer()
        {
            return GetCurrentEuchreGame().CurrentPlayer;
        }
        public void SetCurrentPlayer(Player player)
        {
            GetCurrentEuchreGame().CurrentPlayer = player;
        }
        public void SetTrump(Suit trumpSuit)
        {
            var currentGame = GetCurrentEuchreGame();
            currentGame.SetTrump(trumpSuit);
        }
        public void SetKitty(Stack<Card> kittyStack)
        {
            var currentGame = GetCurrentEuchreGame();
            currentGame.SetKitty(kittyStack);
        }
        public void AddPoints(Team team, int pointsEarned)
        {
            team.AddScore(pointsEarned);
        }
        public int GetPoints(Team team)
        {
            return team.Score;
        }
    }
    public interface IGameplayRepository
    {
        EuchreGame GetCurrentEuchreGame();
        void CreateTeams(string team1Player1, string team1Player2, string team2Player1, string team2Player2);
        void GivePlayerCard(Player playerReceivingCard, Card cardToReceive);
        void RemovePlayerCard(Player playerRemovingCard, Card cardToRemove);
        void AddCardToTrick(Player player, Card card);
        void SetTrickSuit(Suit trickSuit);
        void ClearTrick();
        List<Player> GetPlayerPasses();
        void PlayerPass(Player player);
        Player GetCurrentPlayer();
        void SetCurrentPlayer(Player player);
        void SetTrump(Suit trumpSuit);
        void SetKitty(Stack<Card> kittyStack);
        void AddPoints(Team team, int pointsEarned);
        int GetPoints(Team team);
    }
}
