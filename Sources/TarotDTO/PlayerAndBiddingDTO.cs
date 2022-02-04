using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarotDTO
{
    public class PlayerAndBiddingDTO : IDTO
    {

        public PlayerDTO Player { get; set; }
        public Bidding Bidding { get; set; }
    }
}
