using System;
using System.Collections.Generic;
using Utils;

namespace Model
{
    public static class PlayerExtensions
    {
        public static bool StartsWith(this Player player, string substring)
        {
            if(player.FirstName.StartsWith(substring, StringComparison.CurrentCultureIgnoreCase)
                || player.LastName.StartsWith(substring, StringComparison.CurrentCultureIgnoreCase)
                || player.NickName.StartsWith(substring, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static bool Contains(this Player player, string substring)
        {
            if(player.FirstName.ContainsIgnoreCase(substring)
                || player.LastName.ContainsIgnoreCase(substring)
                || player.NickName.ContainsIgnoreCase(substring))
            {
                return true;
            }
            return false;
        }


    }
    public class PlayerNamesEqualityComparer : IComparer<Player>
    {
        private string Substring { get; set; } 
        public PlayerNamesEqualityComparer(string substring)
        {
            Substring = substring;
        }

        public int Compare(Player x, Player y)
        {
            if(x.LastName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase) && !y.LastName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase)) return -1;
            if(!x.LastName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase) && y.LastName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase)) return 1;

            if(x.FirstName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase) && !y.FirstName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase)) return -1;
            if(!x.FirstName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase) && y.FirstName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase)) return 1;
                
            if(x.NickName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase) && !y.NickName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase)) return -1;
            if(!x.NickName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase) && y.NickName.StartsWith(Substring, StringComparison.CurrentCultureIgnoreCase)) return 1;

            if(x.LastName.ContainsIgnoreCase(Substring) && !y.LastName.ContainsIgnoreCase(Substring)) return -1;
            if(!x.LastName.ContainsIgnoreCase(Substring) && y.LastName.ContainsIgnoreCase(Substring)) return 1;

            if(x.FirstName.ContainsIgnoreCase(Substring) && !y.FirstName.ContainsIgnoreCase(Substring)) return -1;
            if(!x.FirstName.ContainsIgnoreCase(Substring) && y.FirstName.ContainsIgnoreCase(Substring)) return 1;
                
            if(x.NickName.ContainsIgnoreCase(Substring) && !y.NickName.ContainsIgnoreCase(Substring)) return -1;
            if(!x.NickName.ContainsIgnoreCase(Substring) && y.NickName.ContainsIgnoreCase(Substring)) return 1;

            return 0;
        }
    }


}
