using System;

namespace GraphQLApi.Inputs
{
    public record GameDTOInput(long Id, DateTime date, int takerPoints, bool excuse, bool twentyOne, PlayerAndBiddingDTOInput[] players);
}
