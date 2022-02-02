﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarotDTO
{
    public class GameDTO
    {
        public long Id { get; private set; } = 0;

        public DateTime Date { get; private set; }

        public int TakerPoints { get; private set; }

        public bool? Excuse { get; private set; }

        public bool? TwentyOne { get; private set; }

        public List<PlayerAndBiddingDTO> Players { get; set; } = new List<PlayerAndBiddingDTO>();
    }
}
