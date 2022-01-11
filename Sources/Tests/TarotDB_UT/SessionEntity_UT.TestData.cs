using System;
using System.Collections.Generic;
using TarotDB;

namespace TarotDB_UT
{
    public class TestData_Session
    {
        public static IEnumerable<object[]> Sessions
        {
            get
            {
                //all good
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), null,
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //empty name
                yield return new object[]
                {
                    "", new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //white name
                yield return new object[]
                {
                    "    ", new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //null name
                yield return new object[]
                {
                    null, new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //no players
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                };
                //1 player
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                };
                //2 identical players
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //2 valid dates
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1981, 6, 25), 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //1 valid date and no starting date
                yield return new object[]
                {
                    "les semi-croustillants", null, new DateTime(1981, 6, 25), 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                //1 valid date and no ending date
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
                yield return new object[]
                {
                    "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new PlayerEntity{FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null },
                    new PlayerEntity {Id = 3 },
                    new PlayerEntity{FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null },
                    new PlayerEntity {Id = 9 },
                    new PlayerEntity{FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null },
                };
            }
        }
    }
}
