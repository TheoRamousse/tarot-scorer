using GraphQL.Types;
using TarotDTO;

namespace GraphQLApi.GraphTypes
{
    public class PlayerGraphType : ObjectGraphType<PlayerDTO>
    {
        public PlayerGraphType()
        {
            Field(x => x.Id).Description("Id of the player");
            Field(x => x.FirstName).Description("First name of the player");
            Field(x => x.LastName).Description("Last name of the player");
            Field(x => x.NickName).Description("Nickname of the player");
        }
    }
}