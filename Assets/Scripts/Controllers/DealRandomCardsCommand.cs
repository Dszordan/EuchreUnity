using Assets.Scripts.Signals;
using Euchre.BusinessLogic;
using strange.extensions.command.impl;

namespace Euchre.Controllers
{
    class DealRandomCardsCommand : Command
    {
        [Inject]
        public IEuchrePlays EuchreEngine { get; set; }
        [Inject]
        public CardsDealtSignal CardsDealtSignal { get; set; }

        public override void Execute()
        {
            Retain();
            EuchreEngine.DealCards();
            var currentEuchreGame = EuchreEngine.GetCurrentEuchreGame();
            var player1 = currentEuchreGame.Team1.Player1;
            var player2 = currentEuchreGame.Team1.Player2;
            var player3 = currentEuchreGame.Team2.Player1;
            var player4 = currentEuchreGame.Team2.Player2;
            CardsDealtSignal.Dispatch(player1);
            CardsDealtSignal.Dispatch(player2);
            CardsDealtSignal.Dispatch(player3);
            CardsDealtSignal.Dispatch(player4);
            Release();
        }
    }
}
