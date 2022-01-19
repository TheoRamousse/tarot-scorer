using GraphQL.Types;
using Model;
using TarotDTO;

namespace GraphQLApi.Queries
{
    public class GameGraphType : ObjectGraphType<GameDTO>
    {
        public GameGraphType(GameDTO game)
        {
            Field(x => x.Id).Description("Game id.");
            Field(x => x.TakerPoints).Description("Number of taker points.");
            Field(x => x.TwentyOne).Description("Is twenty one is this game.");
            Field(x => x.Excuse).Description("Is excuse in this game.");
            Field(x => x.Date).Description("Date of this game.");

            Field<ListGraphType<PlayerAndBiddingGraphType>>(
                "players",
                resolve: context => game.Players
            );
        }
    }
}