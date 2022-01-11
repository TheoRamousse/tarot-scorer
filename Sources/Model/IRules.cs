using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// interface for every different Rules
    /// </summary>
    public interface IRules : IEquatable<IRules>
    {
        /// <summary>
        /// checks if a game is valid (number of players, biddings, ...)
        /// </summary>
        /// <param name="game">game whose validity have to be checked</param>
        /// <param name="validity">validity error code</param>
        /// <returns>true if valid</returns>
        bool CheckValid(Game game, out Validity validity);

        /// <summary>
        /// calculates the scores of the players of a Game
        /// </summary>
        /// <param name="game">the game whose scores are calculated</param>
        /// <returns>scores of the players (player-score pairs)</returns>
        IReadOnlyDictionary<Player, int> GetScores(Game game);

        /// <summary>
        /// minimum number of players with these rules
        /// </summary>
        int MinNbPlayers { get; }

        /// <summary>
        /// maximum number of players with these rules
        /// </summary>
        int MaxNbPlayers { get; }

        /// <summary>
        /// minimum number of players in order to allow king called
        /// </summary>
        /// <remarks>is it really useful?</remarks>
        int MinNbPlayersForKingCalled { get; }

        /// <summary>
        /// maximum number of players that can be king called
        /// </summary>
        int MaxNbKingsCalled { get; }

        /// <summary>
        /// name of these rules
        /// </summary>
        string Name { get; }
    }
}
