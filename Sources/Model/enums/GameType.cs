using System;
namespace Model
{
    /// <summary>
    /// indicates if it is a 3, 4, 5 players game or all games.
    /// </summary>
    /// <remarks>used to filter games by number of players</remarks>
    public enum GameType
    {
        Unknown,
        ThreePlayers,
        FourPlayers,
        FivePlayers,
        All
    }
}
