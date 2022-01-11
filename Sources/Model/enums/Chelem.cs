using System;
namespace Model
{
    /// <summary>
    /// the different values of Chelem. It can be a success, a fail or unknwon.
    /// It can also be announced or not announced.
    /// </summary>
    public enum Chelem
    {
        Unknown = 0,
        Announced = 1,
        Success = 2,
        Fail = 4,
        NotAnnouncedSuccess = Success,
        AnnouncedSuccess = Announced | Success,
        AnnouncedFail = Announced | Fail,
    }
}
