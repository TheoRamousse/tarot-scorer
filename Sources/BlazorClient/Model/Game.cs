using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model
{
    public class Game
    {
        public long Id { get; set; }

        public string Date { get; set; }

        public long TakerPoints { get; set; }

        public bool Excuse { get; set; }

        public bool TwentyOne { get; set; }

        public string PetitResult { get; set; }

        public string Poignée { get; set; }

        public string Chelem { get; set; }

        public List<Player> Players { get; set; }

        public string NbPlayers { get; set; }

        public string Rules { get; set; }

    }
}
