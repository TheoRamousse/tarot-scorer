using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDTO;

namespace GraphQLApi.Queries
{
    public class PlayerAndBiddingGraphType : ObjectGraphType<PlayerAndBiddingDTO>
    {
        public PlayerAndBiddingGraphType()
        {
            Field(x => x.Bidding).Description("Bidding associated to the player.");
            Field<PlayerGraphType>(
                "players",
                resolve: context => context.Source.Player
            );
        }
    }
}
