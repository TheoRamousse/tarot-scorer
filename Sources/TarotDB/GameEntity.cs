using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TarotDB
{
    public class GameEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime DateTime { get; set; }

        public int TakerPoints { get; set; }

        public bool? Excuse { get; set; }

        public bool? TwentyOne { get; set; }

        public PetitResult Petit { get; set; }

        public Poignée Poignée { get; set; }

        public Chelem Chelem { get; set; }

        public string Rules { get; set; }

        public ICollection<PlayerBiddingEntity> Biddings { get; set; } = new List<PlayerBiddingEntity>();

    }
}
