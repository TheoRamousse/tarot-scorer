using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Utils;

[assembly:InternalsVisibleTo("TarotDB_Tests")]
[assembly:InternalsVisibleTo("TarotDB2Model")]
namespace TarotDB
{
    public class PlayerEntity
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string ImageName { get; set; }

        public ICollection<PlayerSessionEntity> Sessions { get; set; } = new List<PlayerSessionEntity>();

        public ICollection<PlayerBiddingEntity> Games { get; set; } = new List<PlayerBiddingEntity>();
    }
}
