using Euchre.BusinessLogic;
using Euchre.Models;

namespace Euchre.Infrastructure
{
    public class StateRepositoryFactory
    {
        public static IStateRepository Create()
        {
            return new StateRepository();
        }
    }
    class StateRepository : IStateRepository
    {
        public EuchreGameState GetCurrentEuchreGameState()
        {
            return StateController.CurrentEuchreGameState;
        }
        public EuchreGameState GetPreviousEuchreGameState()
        {
            return StateController.PreviousEuchreGameState;
        }
        public InterfaceGameState GetCurrentInterfaceGameState()
        {
            return StateController.CurrentInterfaceState;
        }
        public InterfaceGameState GetPreviousInterfaceGameState()
        {
            return StateController.PreviousInterfaceState;
        }
        public GameState GetCurrentGameState()
        {
            return StateController.GetCurrentGameState();
        }
        public void SetEuchreGameState(EuchreGameState euchreGameState)
        {
            StateController.ChangeCurrentEuchreGameState(euchreGameState);
        }
        public void SetInterfaceGameState(InterfaceGameState interfaceGameState)
        {
            StateController.ChangeCurrentInterfaceState(interfaceGameState);
        }
    }
    public interface IStateRepository
    {
        EuchreGameState GetCurrentEuchreGameState();
        EuchreGameState GetPreviousEuchreGameState();
        InterfaceGameState GetCurrentInterfaceGameState();
        InterfaceGameState GetPreviousInterfaceGameState();
        GameState GetCurrentGameState();
        void SetEuchreGameState(EuchreGameState euchreGameState);
        void SetInterfaceGameState(InterfaceGameState interfaceGameState);
    }

}
