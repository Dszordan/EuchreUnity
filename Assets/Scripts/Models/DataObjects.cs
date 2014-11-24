namespace Euchre.BusinessLogic
{
    public enum InterfaceGameState
    {
        MainMenu,
        CreateTeams,
        Playing
    }
    public enum EuchreGameState
    {
        GameNotStarted,
        Dealing,
        ChoosingTrump,
        Playing,
        Scoring,
        GameEnd
    }
    public enum Suit
    {
        Undecided,
        Heart,
        Spade,
        Club,
        Diamond
    }
}