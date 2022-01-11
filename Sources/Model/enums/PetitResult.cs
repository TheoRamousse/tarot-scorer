using System;
namespace Model
{
    /// <summary>
    /// how it ends for the Petit... 
    /// </summary>
    public enum PetitResult
    {
        Unknown = 0,        //0000 0000
        Owned = 1,          //0000 0001
        NotOwned = 2,       //0000 0010
        Saved = 5,          //0000 0101
        Lost = 10,          //0000 1010
        Hunted = 17,        //0001 0001
        AuBout = 32,        //0010 0000
        SavedAuBout = Owned | Saved | AuBout,
        LostAuBout = NotOwned | Lost | AuBout,
        HuntedAuBout = Owned | Hunted | AuBout
    }
}
