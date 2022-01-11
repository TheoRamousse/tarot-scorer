using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Model
{
    public class Session : IEquatable<Session>
    {
        public long Id { get; private set; }

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

        public ReadOnlyCollection<Player> Players { get; private set; }
        private List<Player> players = new List<Player>();

        public Session(long id, string name, DateTime? start, DateTime?  end, params Player[] players)
            : this(name, start, end, players)
        {
            Id = id;
        }

        public Session(string name, DateTime? start, DateTime?  end, params Player[] players)
        {
            Name = name;
            StartingTime = start;
            EndingTime = end;
            if(StartingTime.HasValue && EndingTime.HasValue
                && EndingTime.Value <= StartingTime.Value)
            {
                throw new ArgumentException("EndingTime can not be anterior to StartingTime");
            }
            Players = new ReadOnlyCollection<Player>(this.players);
            AddPlayers(players);
        }

        public bool AddPlayer(Player player)
        {
            if(player == null) return false;
            if(Players.Contains(player))
                return false;
            players.Add(player);
            return true;
        }

        public bool AddPlayers(params Player[] players)
        {
            if(players.Count() == 0) return false;
            var distincts = players.Distinct();
            var toBeAdded = distincts.Except(this.Players);
            if(toBeAdded.Count() == 0) return false;
            this.players.AddRange(toBeAdded);
            return true;
        }

        public bool RemovePlayer(Player player)
        {
            if(player == null) return false;
            if(!players.Contains(player))
                return false;
            players.Remove(player);
            return true;
        }

        public void ClearPlayers()
        {
            players.Clear();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Name} ({DateTimeToString(StartingTime, true)} -> {DateTimeToString(EndingTime, false)})\n");
            sb.Append($"players:\n");
            foreach(Player p in Players)
            {
                sb.Append($"\t{p}");
            }
            return sb.ToString();
        }

        private static string DateTimeToString(DateTime? datetime, bool startOrEnd)
        {
            if(!datetime.HasValue)
            {
                string startOrEndStr = startOrEnd ? "-" : "+";
                return $"{startOrEndStr}{'\u221E'}";
            }
            return $"{datetime.Value.ToString("dd/MM/yyyy - hh:mm")}";

        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(obj, null)) return false;
            if(ReferenceEquals(obj, this)) return true;
            if(GetType() != obj.GetType()) return false;
            return Equals(obj as Session);
        }

        public override int GetHashCode()
        {
            if(Id != 0) return (int)(Id%997);
            return Name.GetHashCode();
        }

        public bool Equals(Session other)
        {
            if(Id != 0) return Id == other.Id;
            if(other.Id != 0) return false;
            return Name.Equals(other.Name)
                && EndingTime == other.EndingTime
                && StartingTime == other.StartingTime
                && Players.SequenceEqual(other.Players);
        }
    }
}
