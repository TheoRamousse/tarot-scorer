using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarotDTO
{
    public class GameDTO
    {
        public long Id { get; private set; } = 0;

        public DateTime Date { get;  set; }

        public int TakerPoints { get;  set; }

        public bool? Excuse { get;  set; }

        public bool? TwentyOne { get;  set; }

        public List<PlayerAndBiddingDTO> Players { get; set; } = new List<PlayerAndBiddingDTO>();
    }
}
