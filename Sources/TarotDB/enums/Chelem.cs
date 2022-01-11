using System;
namespace TarotDB
{
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
