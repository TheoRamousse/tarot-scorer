using System;
namespace Model
{
    /// <summary>
    /// error values that can be thrown when creating a game
    /// </summary>
    public enum Validity
    {
        Unknown,
        Valid,
        NotEnoughPlayers,
        TooManyPlayers,
        NoTaker,
        TooManyTakers,
        ShouldNotHaveKingCalled,
        TooManyKingsCalled,
        PlayerShallHaveBidding,
    }
}
