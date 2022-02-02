using System;

namespace GraphQLApi.Inputs
{
    public record GameDTOInput(DateTime date, int takerPoints, bool excuse, bool twentyOne, PlayerAndBiddingDTOInput[] players);
}
