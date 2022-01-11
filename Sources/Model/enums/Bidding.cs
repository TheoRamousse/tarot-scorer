using System;
namespace Model
{
    /// <summary>
    /// type of bidding of a Player
    /// </summary>
    /// <remarks>The taker has a value of Prise, but which can be precised with a value of Petite, Pousse, Garde, Garde Sans or Garde Contre.
    /// The other players are Opponents. One of them can eventually be called by the King in a game of 5 players.</remarks>
    public enum Bidding
    {
        None = 0,           //0000 0000
        Prise = 1,          //0000 0001
        Petite = 3,         //0000 0011 
        Pousse = 5,         //0000 0101
        Garde = 7,          //0000 0111
        GardeSans = 9,      //0000 1001
        GardeContre = 11,   //0000 1011
        Opponent = 16,      //0001 0000
        KingCalled = 32,    //0010 0000
    }
}
