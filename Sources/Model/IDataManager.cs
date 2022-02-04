using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDB;

namespace Model
{
    /// <summary>
    /// persistance dependency for players, games and sessions
    /// </summary>
    public interface IDataManager : IDisposable
    {
        /// <summary>
        /// gets the total number of players
        /// </summary>
        /// <returns>total number of players</returns>
        Task<int> GetNbPlayers();

        /// <summary>
        /// gets count players (page <i>index+1</i>)
        /// </summary>
        /// <param name="index">number of pages (each page contains <i>count</i> players)</param>
        /// <param name="count">number of players to get</param>
        /// <returns>players</returns>
        Task<IEnumerable<Player>> GetPlayers(int index, int count);

        /// <summary>
        /// gets the player with the corresponding id
        /// </summary>
        /// <param name="id">id of the player to retrieve</param>
        /// <returns>the player with the corresponding id if it exists, null if not</returns>
        Task<Player> GetPlayerById(long id);

        /// <summary>
        /// gets the players with a particular substring in the first name, the last name or the nick name
        /// </summary>
        /// <param name="substring">string to look for in the first name, the last name or the nick name (whatever the case)</param>
        /// <param name="index">number of pages (each page contains <i>count</i> players)</param>
        /// <param name="count">number of players to get</param>
        /// <returns>the players found, empty collection if none found</returns>
        Task<IEnumerable<Player>> GetPlayersByName(string substring, int index, int count);

        /// <summary>
        /// adds a player to the data layer
        /// </summary>
        /// <param name="player">the player to add (it should have an id with the value 0)</param>
        /// <returns>true if added correctly, false if not</returns>
        Task<bool> AddPlayer(Player player);

        /// <summary>
        /// removes a player from the data layer
        /// </summary>
        /// <param name="player">the player to remove</param>
        /// <returns>true if removed correctly, false if not</returns>
        Task<bool> DeletePlayer(Player player);

        /// <summary>
        /// deletes a player from the data layer by giving its id
        /// </summary>
        /// <param name="id">id of the player to remove</param>
        /// <returns>true if removed correctly, false if not</returns>
        Task<bool> DeletePlayer(long id);

        /// <summary>
        /// updates the player with the corresponding id in the data layer, with the properties of a given player
        /// </summary>
        /// <param name="id">id of the player to update</param>
        /// <param name="player">new values to apply to the previous player</param>
        /// <returns>true if the player has been updated correctly, false if not</returns>
        Task<bool> UpdatePlayer(long id, Player player);


        /// <summary>
        /// gets the total number of sessions
        /// </summary>
        /// <returns>the total number of sessions</returns>
        Task<int> GetNbSessions();

        /// <summary>
        /// gets count sessions (page index+1)
        /// </summary>
        /// <param name="index">number of pages (each page contains count sessions)</param>
        /// <param name="count">number of sessions to get</param>
        /// <returns>sessions</returns>
        Task<IEnumerable<Session>> GetSessions(int index, int count);

        /// <summary>
        /// gets the session with the corresponding id
        /// </summary>
        /// <param name="id">id of the session to retrieve</param>
        /// <returns>the session with the corresponding id if it exists, null if not</returns>
        Task<Session> GetSessionById(long id);

        /// <summary>
        /// gets the sessions with a particular substring in their names or in the first names, the last names or the nick names of its players
        /// </summary>
        /// <param name="substring">string to look for in the name of the session or first names, the last names or the nick names of its players (whatever the case)</param>
        /// <param name="index">number of pages (each page contains count sessions)</param>
        /// <param name="count">number of sessions to get</param>
        /// <returns>the sessions found, empty collection if none found</returns>
        Task<IEnumerable<Session>> GetSessionsByName(string substring, int index, int count);

        /// <summary>
        /// gets sessions containing a particular player
        /// </summary>
        /// <param name="player">tbe player to look for in the sessions to get</param>
        /// <param name="index">number of pages (each page contains count sessions)</param>
        /// <param name="count">number of sessions to get</param>
        /// <returns>the sessions found, empty collection if none found</returns>
        Task<IEnumerable<Session>> GetSessionsByPlayer(Player player, int index, int count);

        /// <summary>
        /// adds a session to the data layer
        /// </summary>
        /// <param name="session">the session to add (it should have an id with the value 0)</param>
        /// <returns>true if added correctly, false if not</returns>
        Task<bool> AddSession(Session session);

        /// <summary>
        /// removes a session from the data layer
        /// </summary>
        /// <param name="session">the session to remove</param>
        /// <returns>true if removed correctly, false if not</returns>
        Task<bool> DeleteSession(Session session);

        /// <summary>
        /// deletes a session from the data layer by giving its id
        /// </summary>
        /// <param name="id">id of the session to remove</param>
        /// <returns>true if removed correctly, false if not</returns>
        Task<bool> DeleteSession(long id);

        /// <summary>
        /// updates the session with the corresponding id in the data layer, with the properties of a given session
        /// </summary>
        /// <param name="id">id of the session to update</param>
        /// <param name="session">new values to apply to the previous session</param>
        /// <returns>true if the session has been updated correctly, false if not</returns>
        Task<bool> UpdateSession(long id, Session session);



        /// <summary>
        /// gets the total number of games
        /// </summary>
        /// <returns>the total number of games</returns>
        Task<int> GetNbGames();

        /// <summary>
        /// gets count games (page index+1)
        /// </summary>
        /// <param name="index">number of pages (each page contains <i>count</i> games)</param>
        /// <param name="count">number of games to get</param>
        /// <returns>games</returns>
        Task<IEnumerable<Game>> GetGames(int index, int count);

        /// <summary>
        /// gets the game with the corresponding id
        /// </summary>
        /// <param name="id">id of the game to retrieve</param>
        /// <returns>the game with the corresponding id if it exists, null if not</returns>
        Task<Game> GetGameById(long id);

        /// <summary>
        /// gets all the games that happens in a particular period of time
        /// </summary>
        /// <param name="startTime">date after which games will be retrieved (if null, no inferior limit)</param>
        /// <param name="endTime">date before which games will be retrieved (if null, no superior limit)</param>
        /// <param name="index">number of pages (each page contains <i>count</i> games)</param>
        /// <param name="count">number of games to get</param>
        /// <returns>games</returns>
        Task<IEnumerable<Game>> GetGamesByDate(DateTime? startTime, DateTime? endTime, int index, int count);

        /// <summary>
        /// gets all the games of a given session
        /// </summary>
        /// <param name="session">Session whose games will be retrived</param>
        /// <param name="index">number of pages (each page contains <i>count</i> games)</param>
        /// <param name="count">number of games to get</param>
        /// <returns>games</returns>
        Task<IEnumerable<Game>> GetGamesBySession(Session session, int index, int count);

        /// <summary>
        /// gets all the games played by a particular player
        /// </summary>
        /// <param name="player">player whose games will be retrieved</param>
        /// <param name="index">number of pages (each page contains <i>count</i> games)</param>
        /// <param name="count">number of games to get</param>
        /// <returns>games</returns>
        Task<IEnumerable<Game>> GetGamesByPlayer(Player player, int index, int count);

        /// <summary>
        /// gets all the games played by exactly these players (same number of players, same players)
        /// </summary>
        /// <param name="players">the players whose games will be retrieved</param>
        /// <param name="index">number of pages (each page contains <i>count</i> games)</param>
        /// <param name="count">number of games to get</param>
        /// <returns>games</returns>
        Task<IEnumerable<Game>> GetGamesByPlayers(IEnumerable<Player> players, int index, int count);

        /// <summary>
        /// adds a Game to the data layer
        /// </summary>
        /// <param name="game">the Game to add (it should have an id with the value 0)</param>
        /// <returns>true if added correctly, false if not</returns>
        Task<bool> AddGame(Game game);

        /// <summary>
        /// removes a Game from the data layer
        /// </summary>
        /// <param name="game">the Game to remove</param>
        /// <returns>true if removed correctly, false if not</returns>
        Task<bool> DeleteGame(Game game);

        /// <summary>
        /// deletes a Game from the data layer by giving its id
        /// </summary>
        /// <param name="id">id of the Game to remove</param>
        /// <returns>true if removed correctly, false if not</returns>
        Task<bool> DeleteGame(long id);

        /// <summary>
        /// updates the Game with the corresponding id in the data layer, with the properties of a given Game
        /// </summary>
        /// <param name="id">id of the Game to update</param>
        /// <param name="game">new values to apply to the previous Game</param>
        /// <returns>true if the Game has been updated correctly, false if not</returns>
        Task<Game> UpdateGame(long id, Game game);
    }
}
