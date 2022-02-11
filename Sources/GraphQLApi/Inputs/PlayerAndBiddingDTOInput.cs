using Model;

namespace GraphQLApi.Inputs
{
    public record PlayerAndBiddingDTOInput(PlayerDTOInput player, Bidding bidding);
}
