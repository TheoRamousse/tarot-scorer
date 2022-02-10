using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// stores information about a Game, including its players, players biddings and rules
    /// </summary>
    public partial class Game
    {
        /// <summary>
        /// unique id of this Game
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Date and time of the game
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// points realized by the taker.
        /// </summary>
        /// <remarks>these points are only the tricks points.
        /// Do not consider oudler points, chelem, poignée, bonus, ...
        /// The bonus points will be automatically calculated.</remarks>
        public int TakerPoints { get; private set; }

        /// <summary>
        /// indicates if the taker has ended the game with the Excuse
        /// </summary>
        /// <remarks>null if it is unknown</remarks>
        public bool? Excuse { get; private set; }

        /// <summary>
        /// indicates if the taker has ended the game with the 21
        /// </summary>
        /// <remarks>null if it is unknown</remarks>
        public bool? TwentyOne { get; private set; }

        /// <summary>
        /// indicates how it has ended for the Petit (1)
        /// </summary>
        /// <see cref="PetitResult"/>
        public PetitResult PetitResult { get; private set; }

        /// <summary>
        /// indicates if a Poignée has been announced or not, and if it has been a success or not
        /// </summary>
        /// <see cref="Poignée"/>
        public Poignée Poignée { get; private set; }

        /// <summary>
        /// indicates if a Chelem has been announced or not, and if it has been a success or not
        /// </summary>
        public Chelem Chelem { get; private set; }

        /// <summary>
        /// the Players and their associated bidding
        /// </summary>
        public ReadOnlyDictionary<Player, Bidding> Players { get; set; }
        private Dictionary<Player, Bidding> players = new Dictionary<Player, Bidding>();

        /// <summary>
        /// number of players of this Game
        /// </summary>
        public int NbPlayers => Players.Count;

        /// <summary>
        /// rules of this Game
        /// </summary>
        public IRules Rules
        {
            get => rules;
            set => rules = value;
        }
        private IRules rules;

        /// <summary>
        /// Scores for each Player in this Game
        /// </summary>
        /// <remarks>every time you call this getter property, scores are calculated</remarks>
        public IReadOnlyDictionary<Player, int> Scores
        {
            get
            {
                if(Rules == null)
                    return Players.ToDictionary(player => player.Key, player => 0);
                return Rules.GetScores(this);
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of this Game</param>
        /// <param name="date">date and time of this game</param>
        /// <param name="rules">rules of this Game</param>
        /// <param name="takerPoints">tricks points only made by the taker</param>
        /// <param name="petitResult">petit result in this game</param>
        /// <param name="poignée">poignée success and announcement</param>
        /// <param name="excuse">does the taker has the Excuse? (null if unknown)</param>
        /// <param name="twentyOne">does the taker has the 21? (null if unknown)</param>
        /// <param name="chelem">Chelem success and announcement</param>
        /// <param name="biddings">players and their associated bidding</param>
        public Game(long id, DateTime date, IRules rules, int takerPoints,
                    PetitResult petitResult = PetitResult.Unknown,
                    Poignée poignée = Poignée.Unknown, bool? excuse = null,
                    bool? twentyOne = null, Chelem chelem = Chelem.Unknown,
                    params Tuple<Player, Bidding>[] biddings)
            : this(date, rules, takerPoints, petitResult, poignée, excuse, twentyOne, chelem, biddings)
        {
            Id = id;
        }

        public Game() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="date">date and time of this game</param>
        /// <param name="rules">rules of this Game</param>
        /// <param name="takerPoints">tricks points only made by the taker</param>
        /// <param name="petitResult">petit result in this game</param>
        /// <param name="poignée">poignée success and announcement</param>
        /// <param name="excuse">does the taker has the Excuse? (null if unknown)</param>
        /// <param name="twentyOne">does the taker has the 21? (null if unknown)</param>
        /// <param name="chelem">Chelem success and announcement</param>
        /// <param name="biddings">players and their associated bidding</param>
        public Game(DateTime date, IRules rules, int takerPoints,
                    PetitResult petitResult = PetitResult.Unknown,
                    Poignée poignée = Poignée.Unknown, bool? excuse = null,
                    bool? twentyOne = null, Chelem chelem = Chelem.Unknown,
                    params Tuple<Player, Bidding>[] biddings)
        {
            Date = date;
            Rules = rules;
            TakerPoints = takerPoints;
            PetitResult = petitResult;
            Poignée = poignée;
            Excuse = excuse;
            TwentyOne = twentyOne;
            Chelem = chelem;
            Players = new ReadOnlyDictionary<Player, Bidding>(players);
            AddPlayers(biddings);
        }

        /// <summary>
        /// adds a Player and its associated bidding to this Game
        /// </summary>
        /// <param name="player">the Player to be added</param>
        /// <param name="bidding">associated bidding of this Player</param>
        /// <returns>true if added correctly, false if not (Player is null)</returns>
        /// <remarks>if this Player already exists, its bidding is updated with this new entered one</remarks>
        public bool AddPlayer(Player player, Bidding bidding)
        {
            if (player == null) return false;

            players[player] = bidding;
            return true;
        }

        /// <summary>
        /// adds new players and their associated bidding
        /// </summary>
        /// <param name="players">player-bidding pairs</param>
        /// <returns>true if all players have been added, false if no player has been added</returns>
        /// <remarks>- All players are added or none. If only player can not be added, they are all not added.
        /// - if a player already exists, its bidding is updated</remarks>
        public bool AddPlayers(params Tuple<Player, Bidding>[] players)
        {
            if (players.Count() == 0) return false;
            bool canAllBeAdded = players.Select(r => r.Item1 != null).Aggregate(true, (boolean, b) => boolean & b);
            if (!canAllBeAdded) return false;

            foreach (var p in players)
            {
                AddPlayer(p.Item1, p.Item2);
            }
            return true;
        }

        /// <summary>
        /// removes a Player (and its associated bidding) from this Game
        /// </summary>
        /// <param name="player">Player to be removed</param>
        /// <returns>true if the Player has been removed, false if not (this Player was not in this Game for instance)</returns>
        public bool RemovePlayer(Player player)
        {
            if (!players.ContainsKey(player)) return false;

            players.Remove(player);
            return true;
        }

        /// <summary>
        /// removes all Players and their associated biddings, from this Game
        /// </summary>
        public void RemoveAllPlayers()
        {
            players.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"({Id} - {Date.ToString("dd/MM/yyyy hh:mm")}) ");
            var res = Players.OrderBy(r => r.Value);
            var resTaker = res.First();
            return base.ToString();
        }

        /// <summary>
        /// checks if the game is valid (players, number of players, biddings, points, ...)
        /// </summary>
        /// <param name="validity">validity error code</param>
        /// <returns>true if this Game is valid, false if not</returns>
        /// <remarks>the result depends on the Rules</remarks>
        public bool CheckValid(out Validity validity)
        {
            if(Rules == null)
            {
                validity = Validity.Unknown;
                return false;
            }
            return Rules.CheckValid(this, out validity);
        }
    }
}
