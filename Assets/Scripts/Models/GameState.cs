using Euchre.BusinessLogic;
namespace Euchre.Models
{
    public class GameState
    {
        public EuchreGameState CurrentEuchreGameState { get; private set; }
        public InterfaceGameState CurrentInterfaceGameState { get; private set; }
        private GameState() { }
        public GameState(EuchreGameState currentEuchreGameState, InterfaceGameState currentInterfaceGameState)
        {
            CurrentEuchreGameState = currentEuchreGameState;
            CurrentInterfaceGameState = currentInterfaceGameState;
        }
    }
}
