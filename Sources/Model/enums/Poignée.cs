using System;
namespace Model
{
    /// <summary>
    /// number of declared trump cards by the taker
    /// </summary>
    public enum Poignée
    {
        Unknown = 0,
        None = 1,
        Simple = 2,
        Double = 3,
        Triple = 4,
        Defense = 8,
        SimpleDefense = Simple | Defense,
        DoubleDefense = Double | Defense,
        TripleDefense = Triple | Defense
    }
}
