using System;
using System.Collections.Generic;
using System.Linq;
using Euchre.Infrastructure;
using Euchre.Models;

namespace Euchre.BusinessLogic
{
    public class EuchreEngineBusinessLogicLocal : IEuchrePlays
    {
        [Inject]
        public IGameplayRepository GameplayRepository { get; set; }

        public EuchreEngineBusinessLogicLocal()
        {
            StateController.ChangeCurrentInterfaceState(InterfaceGameState.MainMenu);
        }
        public void PlayCard(Player player, Card cardPlayedByPlayer)
        {
            var stateRepository = StateRepositoryFactory.Create();
            if (stateRepository.GetCurrentEuchreGameState() != EuchreGameState.Playing)
            {
                throw new Exception("You can't play a card while not in the playing state.");
            }

            var gameplayRepository = GameplayRepositoryFactory.Create();
            var currentEuchreGame = gameplayRepository.GetCurrentEuchreGame();
            var currentTrick = currentEuchreGame.CurrentTrick;
            if (currentTrick.PlayerPlayed(player))
            {
                throw new Exception("Player has already played in this trick.");
            }

            //If the player plays the first card in a trick, the suit is chosen.
            //If the suit is already chosen, ensure they have followed suit.
            if (currentTrick.CurrentSuit != Suit.Undecided)
            {
                var playerCardList = player.CurrentHand.CardList;
                var playerHasCardInTrickSuit = cardPlayedByPlayer.CardSuit == currentTrick.CurrentSuit;
                if (!playerHasCardInTrickSuit)
                {
                    //Check if the player has played a card that is not following suit of the trick.
                    //Loop through all cards in their hand and if the suit matches that of the current trick, they have not followed suit.
                    var otherCardsOfTrickSuit =
                        playerCardList.FirstOrDefault(playerCard => playerCard.CardSuit == currentTrick.CurrentSuit);
                    if (otherCardsOfTrickSuit != null)
                    {
                        throw new Exception("Player has attempted to play a card that does not follow suit.");
                    }
                }
            }
            gameplayRepository.AddCardToTrick(player, cardPlayedByPlayer);
            gameplayRepository.RemovePlayerCard(player, cardPlayedByPlayer);
            if (currentTrick.CurrentSuit != Suit.Undecided)
            {
                gameplayRepository.SetTrickSuit(cardPlayedByPlayer.CardSuit);
            }
        }
        public void ChooseCard(Player playerMakingChoice, Card card)
        {
            throw new NotImplementedException();
        }
        public void Pass(Player playerPassing)
        {
            var stateRepository = StateRepositoryFactory.Create();
            if (stateRepository.GetCurrentEuchreGameState() != EuchreGameState.ChoosingTrump)
            {
                throw new Exception("Can't pass when the trump isn't being chosen.");
            }

            var gameplayRepository = GameplayRepositoryFactory.Create();
            var listPlayersPassed = gameplayRepository.GetPlayerPasses();
            //Stick the dealer
            if (listPlayersPassed.Count >= 7)
            {
                throw new Exception("Can't pass on this one while playing with Stick the Dealer Rules.");
            }
        }
        public void PassControlToNextPlayer()
        {
            var gameplayRepository = GameplayRepositoryFactory.Create();
            var previousPlayer = gameplayRepository.GetCurrentPlayer();
            //Need to get list of the order of players.
        }
        public void DealCards()
        {
            ClearCards();
            var potentialCardStack = new Stack<Card>();   
            var orderedCardList = new List<Card>
            {
                new Card(9, Suit.Club),
                new Card(10, Suit.Club),
                new Card(11, Suit.Club),
                new Card(12, Suit.Club),
                new Card(13, Suit.Club),
                new Card(1, Suit.Club),
                new Card(9, Suit.Spade),
                new Card(10, Suit.Spade),
                new Card(11, Suit.Spade),
                new Card(12, Suit.Spade),
                new Card(13, Suit.Spade),
                new Card(1, Suit.Spade),
                new Card(9, Suit.Heart),
                new Card(10, Suit.Heart),
                new Card(11, Suit.Heart),
                new Card(12, Suit.Heart),
                new Card(13, Suit.Heart),
                new Card(1, Suit.Heart),
                new Card(9, Suit.Diamond),
                new Card(10, Suit.Diamond),
                new Card(11, Suit.Diamond),
                new Card(12, Suit.Diamond),
                new Card(1, Suit.Diamond),
                new Card(13, Suit.Diamond)
                
            };

            var randomNumberGenerator = new Random();
            for (int i = 0; i < 24; i++)
            {
                var maxNumber = orderedCardList.Count - 1;
                var randomNumber = randomNumberGenerator.Next(0, maxNumber);
                var randomCard = orderedCardList[randomNumber];
                orderedCardList.Remove(randomCard);
                potentialCardStack.Push(randomCard);
                UnityEngine.Debug.Log("Card being put into potential: " + randomCard);
            }

            var currentGame = GameplayRepository.GetCurrentEuchreGame();
            for (var i = 0; i < 5; i++)
            {
                currentGame.Team1.Player1.ReceiveCard(potentialCardStack.Pop());
                currentGame.Team1.Player2.ReceiveCard(potentialCardStack.Pop());
                currentGame.Team2.Player1.ReceiveCard(potentialCardStack.Pop());
                currentGame.Team2.Player2.ReceiveCard(potentialCardStack.Pop());
            }
            currentGame.SetKitty(potentialCardStack);
            potentialCardStack.Clear();
        }
        public void ClearCards()
        {
            var currentGame = GetCurrentEuchreGame();
            currentGame.Team1.Player1.CurrentHand.ClearHand();
            currentGame.Team1.Player2.CurrentHand.ClearHand();
            currentGame.Team2.Player1.CurrentHand.ClearHand();
            currentGame.Team2.Player2.CurrentHand.ClearHand();
        }
        public void CreateTeams(string team1Player1, string team1Player2, string team2Player1, string team2Player2)
        {
            GameplayRepository.CreateTeams(team1Player1, team1Player2, team2Player1, team2Player2);
            var stateRepository = StateRepositoryFactory.Create();
            stateRepository.SetEuchreGameState(EuchreGameState.Dealing);
            stateRepository.SetInterfaceGameState(InterfaceGameState.Playing);
        }
        public EuchreGame GetCurrentEuchreGame()
        {
            return GameplayRepository.GetCurrentEuchreGame();
        }
    }
    public interface IEuchrePlays
    {
        void PlayCard(Player player, Card cardPlayedByPlayer);
        void ChooseCard(Player playerMakingChoice, Card card);
        void PassControlToNextPlayer();
        void Pass(Player playerPassing);
        void DealCards();
        void ClearCards();
        void CreateTeams(string team1Player1, string team1Player2, string team2Player1, string team2Player2);
        EuchreGame GetCurrentEuchreGame();
    }
}