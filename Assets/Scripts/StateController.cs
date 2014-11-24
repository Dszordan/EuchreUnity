using Euchre.BusinessLogic;
using Euchre.Models;
using UnityEngine;

namespace Euchre
{
    public class StateController : MonoBehaviour
    {
        public static InterfaceGameState CurrentInterfaceState { get; private set; }
        public static InterfaceGameState PreviousInterfaceState { get; private set; }
        public static EuchreGameState CurrentEuchreGameState { get; private set; }
        public static EuchreGameState PreviousEuchreGameState { get; private set; }
        public delegate void ChangeState(object callerObject, ChangeStateArguments changeStateArguments);
        public static event ChangeState ChangeStateEvent;

        public static void ChangeCurrentInterfaceState(InterfaceGameState state)
        {
            PreviousInterfaceState = CurrentInterfaceState;
            CurrentInterfaceState = state;
        }
        public static void ChangeCurrentEuchreGameState(EuchreGameState state)
        {
            PreviousEuchreGameState = CurrentEuchreGameState;
            CurrentEuchreGameState = state;
        }
        public static GameState GetCurrentGameState()
        {
            return new GameState(CurrentEuchreGameState, CurrentInterfaceState);
        }
        private static void OnChangeStateEvent(object callerobject, ChangeStateArguments changestatearguments)
        {
            ChangeState handler = ChangeStateEvent;
            if (handler != null) handler(callerobject, changestatearguments);
        }
    }

    public class ChangeStateArguments
    {
        public InterfaceGameState NewState { get; private set; }
        public InterfaceGameState OldState { get; private set; }
        public ChangeStateArguments(InterfaceGameState oldState, InterfaceGameState newState)
        {
            NewState = newState;
            OldState = oldState;
        }
    }
}