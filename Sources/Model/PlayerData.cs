using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Model_UT")]
namespace Model
{
    public class PlayerData : IEquatable<PlayerData>
    {
        public Player Player { get; internal set; }

        public int NbVictories { get; internal set; }

        public int NbLosses { get; internal set; }

        public int NbPoints { get; internal set; }

        public bool Equals(PlayerData other)
        {
            return Player.Equals(other.Player);
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(obj, null)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(GetType() != obj.GetType()) return false;
            return Equals(obj as PlayerData);
        }

        public override int GetHashCode()
        {
            return Player.GetHashCode();
        }
    }
}
