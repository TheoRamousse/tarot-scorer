using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// a Player of the Tarot Game
    /// </summary>
    public class Player : IEquatable<Player>
    {
        /// <summary>
        /// unique identifier of this Player
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// first name of this Player
        /// </summary>
        public string FirstName
        {
            get => firstName;
            private set
            {
                firstName = string.IsNullOrWhiteSpace(value) ? "" : value;
            }
        }
        private string firstName;

        /// <summary>
        /// last name of this Player
        /// </summary>
        public string LastName
        {
            get => lastName;
            private set
            {
                lastName = string.IsNullOrWhiteSpace(value) ? "" : value;
            }
        }
        private string lastName;

        /// <summary>
        /// nick name of this Player
        /// </summary>
        public string NickName
        {
            get => nickName;
            private set
            {
                nickName = string.IsNullOrWhiteSpace(value) ? "" : value;
            }
        }
        private string nickName;

        /// <summary>
        /// image file name of this Player
        /// </summary>
        public string Image
        {
            get => image;
            private set
            {
                image = string.IsNullOrWhiteSpace(value) ? null : value;
            }
        }
        private string image;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">unique identifier of this Player (choose 0 if this is a new Player)</param>
        /// <param name="firstName">first name of this Player</param>
        /// <param name="lastname">last name of this Player</param>
        /// <param name="nickname">nick name of this Player</param>
        /// <param name="image">image file name of this Player</param>
        public Player(long id, string firstName, string lastName, string nickName, string image)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            Image = image;
            if(string.IsNullOrWhiteSpace(FirstName)
                && string.IsNullOrWhiteSpace(LastName)
                && string.IsNullOrWhiteSpace(NickName))
            {
                throw new ArgumentException("A Player shall have at least one first name or one last name or one nick name");
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="firstName">first name of this Player</param>
        /// <param name="lastname">last name of this Player</param>
        /// <param name="nickname">nick name of this Player</param>
        /// <param name="image">image file name of this Player</param>
        /// <remarks>sets automatically the unique identifier to 0</remarks>
        public Player(string firstName, string lastname, string nickname, string image)
            : this(0, firstName, lastname, nickname, image)
        { }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(obj, null)) return false;
            if(ReferenceEquals(obj, this)) return true;
            if(GetType() != obj.GetType()) return false;
            return Equals(obj as Player);
        }

        public override int GetHashCode()
        {
            if(Id != 0) return (int)(Id%997);
            return LastName.GetHashCode();
        }

        public bool Equals(Player other)
        {
            if(Id != 0) return Id == other.Id;
            if(other.Id != 0) return false;
            return FirstName.Equals(other.FirstName)
                && LastName.Equals(other.LastName)
                && NickName.Equals(other.NickName);
        }

        public override string ToString()
            => $"({Id}) {FirstName} \"{NickName}\" {LastName}";

        /// <summary>
        /// short string version of this Player
        /// </summary>
        /// <returns>either its NickName if it exists, or FirstName LastName if not</returns>
        public string ToShortString()
        {
            if(!string.IsNullOrWhiteSpace(NickName)) return NickName;

            string firstname = string.IsNullOrWhiteSpace(FirstName) ? "" : $"{FirstName} ";
            return $"{firstname}{LastName}";
        }



        class PropEqComparer : EqualityComparer<Player>
        {
            public override bool Equals(Player x, Player y)
            {
                return 
                        x.FirstName == y.FirstName
                        && x.LastName == y.LastName
                        && x.NickName == y.NickName
                        && x.Image == y.Image;
            }

            public override int GetHashCode(Player obj)
            {
                return obj.LastName.GetHashCode();
            }
        }

        /// <summary>
        /// allows comparing exact equality of firstnames, lastnames, nicknames and imagenames of two players
        /// </summary>
        public static EqualityComparer<Player> PropertiesComparer { get; } = new PropEqComparer();
    }
}
