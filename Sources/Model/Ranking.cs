using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model
{
    public class Ranking
    {
        public string Name
        {
            get => name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    name = "no name";
                    return;
                }
                name = value;
            }
        }
        private string name;

        public DateTime? StartingTime { get; set; }

        public DateTime? EndingTime { get; set; }

        public GameType Type { get; set; }

        private HashSet<PlayerData> playersData = new HashSet<PlayerData>();

        public Ranking(string name, DateTime? start, DateTime? end, GameType type = GameType.All,
            params PlayerData[] datas)
        {
            Name = name;
            StartingTime = start;
            EndingTime = end;
            if(StartingTime.HasValue && EndingTime.HasValue
                && EndingTime.Value <= StartingTime.Value)
            {
                throw new ArgumentException("EndingTime can not be anterior to StartingTime");
            }
            Type = type;
            AddPlayersData(datas);
        }

        public bool AddPlayerData(PlayerData playerData)
        {
            return playersData.Add(playerData);
        }

        public void AddPlayersData(params PlayerData[] playersData)
        {
            foreach(var p in playersData)
            {
                this.playersData.Add(p);
            }
        }

        public bool UpdatePlayerData(PlayerData playerData)
        {
            if(!playersData.Contains(playerData)) return false;
            playersData.Remove(playerData);
            return playersData.Add(playerData);
        }

        public bool RemovePlayer(Player player)
        {
            PlayerData playerData = new PlayerData()
            {
                Player = player
            };
            return playersData.Remove(playerData);
        }

        public void ClearPlayersData()
        {
            playersData.Clear();
        }

        public IEnumerable<PlayerData> PlayerDatas
            => playersData.ToArray();

        public IEnumerable<PlayerData> RankPlayersBy(Func<IEnumerable<PlayerData>, IEnumerable<PlayerData>> func)
        {
            return func(playersData);
        }
    }

    public static class ClassicRankingMethods
    {
        public static IEnumerable<PlayerData> ByDescendingPoints(this Ranking ranking)
        {
            return ranking.RankPlayersBy(playersData
                => playersData.OrderByDescending(data => data.NbPoints)
                              .ThenBy(data => data.NbVictories)
                              .ThenBy(data => data.NbLosses));
        }

        public static IEnumerable<PlayerData> ByDescendingVictories(this Ranking ranking)
        {
            return ranking.RankPlayersBy(playersData
                => playersData.OrderByDescending(data => data.NbVictories)
                              .ThenBy(data => data.NbLosses)
                              .ThenByDescending(data => data.NbPoints));
        }

        public static IEnumerable<PlayerData> ByMeanPointsPerGame(this Ranking ranking)
        {
            return ranking.RankPlayersBy(playersData
                => playersData.OrderByDescending(data => data.NbPoints / (data.NbVictories + data.NbLosses)));
        }
    }
}
