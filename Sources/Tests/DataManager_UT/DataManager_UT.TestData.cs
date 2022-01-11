using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model;
using TarotDB;
using TarotDB2Model;
using nsModel = Model;

namespace DataManager_UT
{
    public class TestData_DataManager
    {
        private static DbContextOptions<TarotContext> InitDB()
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TarotContext>()
                .UseSqlite(connection)
                .Options;

            return options;
        }

        private static IDataManager InitTarotDbManager()
        {
            var context = new TarotContextStub(InitDB());
            context.Database.EnsureCreated();
            return new TarotDBManager(context);
        }

        public static IEnumerable<object[]> NbPlayers
        {
            get
            {
                yield return new object[]
                {
                    16, new Stub()
                };
                yield return new object[]
                {
                    16, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> GetPlayers
        {
            get
            {
                yield return new object[]
                {
                    16, 0, 20, new Stub()
                };
                yield return new object[]
                {
                    16,  0, 20, InitTarotDbManager()
                };

                yield return new object[]
                {
                    10, 0, 10, new Stub()
                };
                yield return new object[]
                {
                    10,  0, 10, InitTarotDbManager()
                };

                yield return new object[]
                {
                    6, 1, 10, new Stub()
                };
                yield return new object[]
                {
                    6,  1, 10, InitTarotDbManager()
                };

                yield return new object[]
                {
                    0, 2, 10, new Stub()
                };
                yield return new object[]
                {
                    0,  2, 10, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> GetPlayerById
        {
            get
            {
                yield return new object[]
                {
                    -1, new Stub(), new Player(-1, "Jane", "Doe", "", "")
                };
                yield return new object[]
                {
                    -1, InitTarotDbManager(), new Player(-1, "Jane", "Doe", "", "")
                };
                yield return new object[]
                {
                    3, new Stub(), new Player(3, "Stanley", "Clarke", null, null)
                };
                yield return new object[]
                {
                    3, InitTarotDbManager(), new Player(3, "Stanley", "Clarke", null, null)
                };
            }
        }

        public static IEnumerable<object[]> GetPlayersByName
        {
            get
            {
                //search with results
                yield return new object[]
                {
                    "on", 0, 10, new Stub(),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };
                yield return new object[]
                {
                    "on", 0, 10, InitTarotDbManager(),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };

                //search with wrong case
                yield return new object[]
                {
                    "oN", 0, 10, new Stub(),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };
                yield return new object[]
                {
                    "oN", 0, 10, InitTarotDbManager(),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };

                //search with results and index, count tested
                yield return new object[]
                {
                    "on", 0, 2, new Stub(),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    //new Player(7, "Ron", "Carter", null, null),
                };
                yield return new object[]
                {
                    "on", 0, 2, InitTarotDbManager(),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    //new Player(7, "Ron", "Carter", null, null),
                };

                //search with results and index, count tested
                yield return new object[]
                {
                    "on", 1, 2, new Stub(),
                    //new Player(4, "Jean-Luc", "Ponty", null, null),
                    //new Player(6, "Tony", "Williams", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };
                yield return new object[]
                {
                    "on", 1, 2, InitTarotDbManager(),
                    //new Player(4, "Jean-Luc", "Ponty", null, null),
                    //new Player(6, "Tony", "Williams", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };

                //search with result that should not have Jane Doe
                yield return new object[]
                {
                    "an", 0, 10, new Stub(),
                    new Player(3, "Stanley", "Clarke", null, null),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(11, "Herbie", "Hancock", null, null),
                    new Player(13, "Dave", "Holland", null, null),
                };
                yield return new object[]
                {
                    "an", 0, 10, InitTarotDbManager(),
                    new Player(3, "Stanley", "Clarke", null, null),
                    new Player(4, "Jean-Luc", "Ponty", null, null),
                    new Player(11, "Herbie", "Hancock", null, null),
                    new Player(13, "Dave", "Holland", null, null),
                };
            }
        }

        public static IEnumerable<object[]> AddPlayer
        {
            get
            {
                yield return new object[]
                {
                    true, 0,
                    0, "Charlie", "Parker", "Bird", "bird.jpg",
                    new Stub()
                };
                yield return new object[]
                {
                    true, 0,
                    0, "Charlie", "Parker", "Bird", "bird.jpg",
                    InitTarotDbManager()
                };

                yield return new object[]
                {
                    true, 1,
                    0, "Mary Lou", "Williams", "", null,
                    new Stub()
                };
                yield return new object[]
                {
                    true, 1,
                    0, "Mary Lou", "Williams", "", null,
                    InitTarotDbManager()
                };

                yield return new object[]
                {
                    false, 1,
                    6, "Tony", "Williams", null, null,
                    new Stub()
                };
                yield return new object[]
                {
                    false, 1,
                    6, "Tony", "Williams", null, null,
                    InitTarotDbManager()
                };

                yield return new object[]
                {
                    true, 1,
                    0, "Tony", "Williams", "", null,
                    new Stub()
                };
                yield return new object[]
                {
                    true, 1,
                    0, "Tony", "Williams", "", null,
                    InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> DeletePlayer
        {
            get
            {
                yield return new object[]
                {
                    true,
                    9, "Wayne", "Shorter", "", null,
                    new Stub()
                };
                yield return new object[]
                {
                    true,
                    9, "Wayne", "Shorter", "", null,
                    InitTarotDbManager()
                };

                yield return new object[]
                {
                    false,
                    0, "Wayne", "Shorter", "", null,
                    new Stub()
                };
                yield return new object[]
                {
                    false,
                    0, "Wayne", "Shorter", "", null,
                    InitTarotDbManager()
                };

                yield return new object[]
                {
                    true,
                    9, "Someone", "Else", "", null,
                    new Stub()
                };
                yield return new object[]
                {
                    true,
                    9, "Someone", "Someone", "", null,
                    InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> DeletePlayerById
        {
            get
            {
                yield return new object[]
                {
                    true,
                    9, new Stub()
                };
                yield return new object[]
                {
                    true,
                    9, InitTarotDbManager()
                };

                yield return new object[]
                {
                    false,
                    0, new Stub()
                };
                yield return new object[]
                {
                    false,
                    0, InitTarotDbManager()
                };

                yield return new object[]
                {
                    false,
                    29, new Stub()
                };
                yield return new object[]
                {
                    false,
                    29, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> UpdatePlayer
        {
            get
            {
                yield return new object[]
                {
                    true,
                    9, new Player(0, "Charlie", "Parker", "Bird", "bird.jpg"),
                    new Stub()
                };
                yield return new object[]
                {
                    true,
                    9, new Player(0, "Charlie", "Parker", "Bird", "bird.jpg"),
                    InitTarotDbManager()
                };
                yield return new object[]
                {
                    false,
                    29, new Player(0, "Charlie", "Parker", "Bird", "bird.jpg"),
                    new Stub()
                };
                yield return new object[]
                {
                    false,
                    29, new Player(0, "Charlie", "Parker", "Bird", "bird.jpg"),
                    InitTarotDbManager()
                };
                yield return new object[]
                {
                    false,
                    0, new Player(0, "Charlie", "Parker", "Bird", "bird.jpg"),
                    new Stub()
                };
                yield return new object[]
                {
                    false,
                    0, new Player(0, "Charlie", "Parker", "Bird", "bird.jpg"),
                    InitTarotDbManager()
                };
                yield return new object[]
                {
                    false,
                    9, null,
                    new Stub()
                };
                yield return new object[]
                {
                    false,
                    9, null,
                    InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> NbSessions
        {
            get
            {
                yield return new object[]
                {
                    3, new Stub()
                };
                yield return new object[]
                {
                    3, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> GetSessions
        {
            get
            {
                yield return new object[]
                {
                    3, 0, 20, new Stub()
                };
                yield return new object[]
                {
                    3,  0, 20, InitTarotDbManager()
                };

                yield return new object[]
                {
                    2, 0, 2, new Stub()
                };
                yield return new object[]
                {
                    2,  0, 2, InitTarotDbManager()
                };

                yield return new object[]
                {
                    1, 1, 2, new Stub()
                };
                yield return new object[]
                {
                    1, 1, 2, InitTarotDbManager()
                };

                yield return new object[]
                {
                    0, 1, 10, new Stub()
                };
                yield return new object[]
                {
                    0, 1, 10, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> GetSessionById
        {
            get
            {
                yield return new object[]
                {
                    0, new Stub(), null
                };
                yield return new object[]
                {
                    0, InitTarotDbManager(), null
                };
                yield return new object[]
                {
                    10, new Stub(), null
                };
                yield return new object[]
                {
                    10, InitTarotDbManager(), null
                };

                yield return new object[]
                {
                    1, new Stub(), new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                };
                yield return new object[]
                {
                    1, InitTarotDbManager(), new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                };

                yield return new object[]
                {
                    2, new Stub(), new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                };
                yield return new object[]
                {
                    2, InitTarotDbManager(), new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                };

                yield return new object[]
                {
                    3, new Stub(), new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(14, "Miroslav", "Vitous", null, null),
                        new Player(15, "Jaco", "Pastorius", null, null),
                        new Player(16, "Peter", "Erskine", null, null)),
                };
                yield return new object[]
                {
                    3, InitTarotDbManager(), new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(14, "Miroslav", "Vitous", null, null),
                        new Player(15, "Jaco", "Pastorius", null, null),
                        new Player(16, "Peter", "Erskine", null, null)),
                };
            }
        }

        public static IEnumerable<object[]> GetSessionsByName
        {
            get
            {
                //search with results
                yield return new object[]
                {
                    "or", 0, 10, new Stub(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                    new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                    new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(14, "Miroslav", "Vitous", null, null),
                        new Player(15, "Jaco", "Pastorius", null, null),
                        new Player(16, "Peter", "Erskine", null, null)),

                };
                yield return new object[]
                {
                    "or", 0, 10, InitTarotDbManager(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                    new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                    new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(14, "Miroslav", "Vitous", null, null),
                        new Player(15, "Jaco", "Pastorius", null, null),
                        new Player(16, "Peter", "Erskine", null, null)),
                };

                //search with no result
                yield return new object[]
                {
                    "zz", 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    "zz", 0, 10, InitTarotDbManager(),
                };

                //search with results
                yield return new object[]
                {
                    "en", 0, 10, new Stub(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                    new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                };
                yield return new object[]
                {
                    "en", 0, 10, InitTarotDbManager(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                    new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                };

                //search with results
                yield return new object[]
                {
                    "dd", 0, 10, new Stub(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                };
                yield return new object[]
                {
                    "dd", 0, 10, InitTarotDbManager(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                };
            }
        }

        public static IEnumerable<object[]> GetSessionsByPlayer
        {
            get
            {
                //search with 1 player => 2 sessions
                yield return new object[]
                {
                    new Player(9, "Wayne", "Shorter", null, null), 0, 10, new Stub(),
                    new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                    new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(14, "Miroslav", "Vitous", null, null),
                        new Player(15, "Jaco", "Pastorius", null, null),
                        new Player(16, "Peter", "Erskine", null, null)),
                };
                yield return new object[]
                {
                    new Player(9, "Wayne", "Shorter", null, null), 0, 10, InitTarotDbManager(),
                    new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(8, "Miles", "Davis", null, null),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(10, "John", "McLaughlin", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(13, "Dave", "Holland", null, null)),
                    new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31),
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(12, "Joe", "Zawinul", null, null),
                        new Player(14, "Miroslav", "Vitous", null, null),
                        new Player(15, "Jaco", "Pastorius", null, null),
                        new Player(16, "Peter", "Erskine", null, null)),
                };

                //search with null player => no result
                yield return new object[]
                {
                    null, 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    null, 0, 10, InitTarotDbManager(),
                };

                //search with 1 player => no result
                yield return new object[]
                {
                    new Player(27, "Charlie", "Parker", "Bird", "bird.jpg"), 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new Player(27, "Charlie", "Parker", "Bird", "bird.jpg"), 0, 10, InitTarotDbManager(),
                };

                //search with 1 player => 1 session
                yield return new object[]
                {
                    new Player(5, "Steve", "Gadd", null, null), 0, 10, new Stub(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                };
                yield return new object[]
                {
                    new Player(5, "Steve", "Gadd", null, null), 0, 10, InitTarotDbManager(),
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                };

                //search with 1 invalid player => no result
                yield return new object[]
                {
                    new Player(0, "Steve", "Gadd", null, null), 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new Player(0, "Steve", "Gadd", null, null), 0, 10, InitTarotDbManager(),
                };
            }
        }

        //bool success, IDataManager dataManager, int expectedId,
        //                                 int id, string name, DateTime? startTime, DateTime? endTime,
        //                                 int expectedNbOfNewPlayers,
        //                                 params Player[] players

        public static IEnumerable<object[]> AddSession
        {
            get
            {
                //add new session with existing players
                yield return new object[]
                {
                    true, new Stub(), 4,
                    0, "Filles de Kilimanjaro", new DateTime(1968, 6, 19), new DateTime(1968, 9, 24),
                    0,
                    new Player(8, "Miles", "Davis", null, null), 
                    new Player(9, "Wayne", "Shorter", null, null),
                    new Player(11, "Herbie", "Hancock", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player(13, "Dave", "Holland", null, null),
                    new Player(2, "Chick", "Corea", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };
                yield return new object[]
                {
                    true, InitTarotDbManager(), 4,
                    0, "Filles de Kilimanjaro", new DateTime(1968, 6, 19), new DateTime(1968, 9, 24),
                    0,
                    new Player(8, "Miles", "Davis", null, null), 
                    new Player(9, "Wayne", "Shorter", null, null),
                    new Player(11, "Herbie", "Hancock", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player(13, "Dave", "Holland", null, null),
                    new Player(2, "Chick", "Corea", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };

                //add new session with new players
                yield return new object[]
                {
                    true, new Stub(), 4,
                    0, "Bozilo", new DateTime(2009, 3, 9), null,
                    3,
                    new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), 
                    new Player("Karim", "Ziad", null, null),
                    new Player("Julien", "Loureau", null, null),
                };
                yield return new object[]
                {
                    true, InitTarotDbManager(), 4,
                    0, "Bozilo", new DateTime(2009, 3, 9), null,
                    3,
                    new Player("Bojan", "Zulfikarpasic", "Bojan Z", null),
                    new Player("Karim", "Ziad", null, null),
                    new Player("Julien", "Loureau", null, null),
                };

                //add new session with new and existing players
                yield return new object[]
                {
                    true, new Stub(), 4,
                    0, "My Funny Valentine", new DateTime(1964, 2, 12), new DateTime(1964, 2, 12, 23, 59, 0),
                    1,
                    new Player(8, "Miles", "Davis", null, null), 
                    new Player(11, "Herbie", "Hancock", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player("George", "Coleman", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };
                yield return new object[]
                {
                    true, InitTarotDbManager(), 4,
                    0, "My Funny Valentine", new DateTime(1964, 2, 12), new DateTime(1964, 2, 12, 23, 59, 0),
                    1,
                    new Player(8, "Miles", "Davis", null, null), 
                    new Player(11, "Herbie", "Hancock", null, null),
                    new Player(6, "Tony", "Williams", null, null),
                    new Player("George", "Coleman", null, null),
                    new Player(7, "Ron", "Carter", null, null),
                };

                //add existing session
                yield return new object[]
                {
                    false, new Stub(), 2,
                    2, null, null, null,
                    0,
                };
                yield return new object[]
                {
                    false, InitTarotDbManager(), 2,
                    2, null, null, null,
                    0,
                };
            }
        }

        public static IEnumerable<object[]> DeleteSession
        {
            get
            {
                //delete new session with existing players
                yield return new object[]
                {
                    false, 
                    new Session(0, "Filles de Kilimanjaro", new DateTime(1968, 6, 19), new DateTime(1968, 9, 24),
                        new Player(8, "Miles", "Davis", null, null), 
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(13, "Dave", "Holland", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(7, "Ron", "Carter", null, null)),
                    new Stub(),
                };
                yield return new object[]
                {
                    false, 
                    new Session(0, "Filles de Kilimanjaro", new DateTime(1968, 6, 19), new DateTime(1968, 9, 24),
                        new Player(8, "Miles", "Davis", null, null), 
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(13, "Dave", "Holland", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(7, "Ron", "Carter", null, null)),
                    InitTarotDbManager(),
                };

                //delete new session with new players
                yield return new object[]
                {
                    false,
                    new Session(0, "Bozilo", new DateTime(2009, 3, 9), null,
                        new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), 
                        new Player("Karim", "Ziad", null, null),
                        new Player("Julien", "Loureau", null, null)),
                    new Stub(),
                };
                yield return new object[]
                {
                    false,
                    new Session(0, "Bozilo", new DateTime(2009, 3, 9), null,
                        new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), 
                        new Player("Karim", "Ziad", null, null),
                        new Player("Julien", "Loureau", null, null)),
                    InitTarotDbManager(),
                };

                //delete new session with new and existing players
                yield return new object[]
                {
                    false,
                    new Session(0, "My Funny Valentine", new DateTime(1964, 2, 12), new DateTime(1964, 2, 12, 23, 59, 0),
                        new Player(8, "Miles", "Davis", null, null), 
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player("George", "Coleman", null, null),
                        new Player(7, "Ron", "Carter", null, null)),
                    new Stub(),
                };
                yield return new object[]
                {
                    false,
                    new Session(0, "My Funny Valentine", new DateTime(1964, 2, 12), new DateTime(1964, 2, 12, 23, 59, 0),
                        new Player(8, "Miles", "Davis", null, null), 
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player("George", "Coleman", null, null),
                        new Player(7, "Ron", "Carter", null, null)),
                    InitTarotDbManager(),
                };

                //delete existing session with existing players
                yield return new object[]
                {
                    true,
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                    new Stub(),
                };
                yield return new object[]
                {
                    true,
                    new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9),
                        new Player(1, "Lenny", "White", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(3, "Stanley", "Clarke", null, null),
                        new Player(4, "Jean-Luc", "Ponty", null, null),
                        new Player(5, "Steve", "Gadd", null, null)),
                    InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> DeleteSessionById
        {
            get
            {
                //delete new session with existing players
                yield return new object[]
                {
                    false, 
                    0,
                    new Stub(),
                };
                yield return new object[]
                {
                    false, 
                    0,
                    InitTarotDbManager(),
                };

                //delete not existing session
                yield return new object[]
                {
                    false,
                    10,
                    new Stub(),
                };
                yield return new object[]
                {
                    false,
                    10,
                    InitTarotDbManager(),
                };

                //delete session 
                yield return new object[]
                {
                    true,
                    1,
                    new Stub(),
                };
                yield return new object[]
                {
                    true,
                    1,
                    InitTarotDbManager(),
                };

                //delete session 
                yield return new object[]
                {
                    true,
                    2,
                    new Stub(),
                };
                yield return new object[]
                {
                    true,
                    2,
                    InitTarotDbManager(),
                };

                //delete session 
                yield return new object[]
                {
                    true,
                    3,
                    new Stub(),
                };
                yield return new object[]
                {
                    true,
                    3,
                    InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> UpdateSession
        {
            get
            {
                //search with results
                yield return new object[]
                {
                    true, 2, 
                    new Session("Filles de Kilimanjaro", new DateTime(1968, 6, 19), new DateTime(1968, 9, 24),
                        new Player(8, "Miles", "Davis", null, null), 
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(13, "Dave", "Holland", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(7, "Ron", "Carter", null, null)),
                    new Stub(),
                };
                yield return new object[]
                {
                    true, 2, 
                    new Session("Filles de Kilimanjaro", new DateTime(1968, 6, 19), new DateTime(1968, 9, 24),
                        new Player(8, "Miles", "Davis", null, null), 
                        new Player(9, "Wayne", "Shorter", null, null),
                        new Player(11, "Herbie", "Hancock", null, null),
                        new Player(6, "Tony", "Williams", null, null),
                        new Player(13, "Dave", "Holland", null, null),
                        new Player(2, "Chick", "Corea", null, null),
                        new Player(7, "Ron", "Carter", null, null)),
                    InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> NbGames
        {
            get
            {
                yield return new object[]
                {
                    5, new Stub()
                };
                yield return new object[]
                {
                    5, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> GetGames
        {
            get
            {
                yield return new object[]
                {
                    5, 0, 20, new Stub()
                };
                yield return new object[]
                {
                    5,  0, 20, InitTarotDbManager()
                };

                yield return new object[]
                {
                    2, 0, 2, new Stub()
                };
                yield return new object[]
                {
                    2,  0, 2, InitTarotDbManager()
                };

                yield return new object[]
                {
                    1, 2, 2, new Stub()
                };
                yield return new object[]
                {
                    1, 2, 2, InitTarotDbManager()
                };

                yield return new object[]
                {
                    0, 1, 10, new Stub()
                };
                yield return new object[]
                {
                    0, 1, 10, InitTarotDbManager()
                };
            }
        }

        public static IEnumerable<object[]> GetGameById
        {
            get
            {
                yield return new object[]
                {
                    0, new Stub(), null
                };
                yield return new object[]
                {
                    0, InitTarotDbManager(), null
                };
                yield return new object[]
                {
                    10, new Stub(), null
                };
                yield return new object[]
                {
                    10, InitTarotDbManager(), null
                };

                yield return new object[]
                {
                    1, new Stub(), new Game(1, new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, true, false, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.GardeSans),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)),
                };
                yield return new object[]
                {
                    1, InitTarotDbManager(), new Game(1, new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, true, false, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.GardeSans),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)),
                };

                yield return new object[]
                {
                    2, new Stub(), new Game(2, new DateTime(2021, 2, 2), new FrenchTarotRules(), 45, Model.PetitResult.LostAuBout, Model.Poignée.None, true, true, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(4).Result, Model.Bidding.Garde),
                            Tuple.Create((new Stub()).GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(6).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(7).Result, Model.Bidding.Opponent)),
                };
                yield return new object[]
                {
                    2, InitTarotDbManager(), new Game(2, new DateTime(2021, 2, 2), new FrenchTarotRules(), 45, Model.PetitResult.LostAuBout, Model.Poignée.None, true, true, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(4).Result, Model.Bidding.Garde),
                            Tuple.Create((new Stub()).GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(6).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(7).Result, Model.Bidding.Opponent)),
                };

                yield return new object[]
                {
                    5, new Stub(), new Game(5, new DateTime(2021, 5, 5), new FrenchTarotRules(), 87, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, false, true, Model.Chelem.AnnouncedSuccess,
                            Tuple.Create((new Stub()).GetPlayerById(10).Result, Model.Bidding.GardeContre),
                            Tuple.Create((new Stub()).GetPlayerById(16).Result, Model.Bidding.KingCalled),
                            Tuple.Create((new Stub()).GetPlayerById(7).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(4).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent)),
                };
                yield return new object[]
                {
                    5, InitTarotDbManager(), new Game(5, new DateTime(2021, 5, 5), new FrenchTarotRules(), 87, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, false, true, Model.Chelem.AnnouncedSuccess,
                            Tuple.Create((new Stub()).GetPlayerById(10).Result, Model.Bidding.GardeContre),
                            Tuple.Create((new Stub()).GetPlayerById(16).Result, Model.Bidding.KingCalled),
                            Tuple.Create((new Stub()).GetPlayerById(7).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(4).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent)),
                };
            }
        }

        //DateTime? start, DateTime? end, int index, int count, IDataManager dataManager,
        //    params Game[] expectedGames

        public static IEnumerable<object[]> GetGamesByDate
        {
            get
            {
                //find all games
                yield return new object[]
                {
                    null, null, 0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    null, null, 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //find all games
                yield return new object[]
                {
                    new DateTime(2020, 12, 31), null, 0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    new DateTime(2020, 12, 31), null, 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //find all games
                yield return new object[]
                {
                    null, new DateTime(2021, 12, 31), 0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    null, new DateTime(2021, 12, 31), 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //find all games
                yield return new object[]
                {
                    new DateTime(2020, 12, 31), new DateTime(2021, 12, 31), 0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    new DateTime(2020, 12, 31), new DateTime(2021, 12, 31), 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //find some games
                yield return new object[]
                {
                    new DateTime(2021, 3, 31), null, 0, 10, new Stub(),
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    new DateTime(2021, 3, 31), null, 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(4).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //find some games
                yield return new object[]
                {
                    null, new DateTime(2021, 3, 31), 0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                };
                yield return new object[]
                {
                    null, new DateTime(2021, 3, 31), 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(3).Result,
                };

                //find some games
                yield return new object[]
                {
                    new DateTime(2021, 2, 15), new DateTime(2021, 4, 15), 0, 10, new Stub(),
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                };
                yield return new object[]
                {
                    new DateTime(2021, 2, 15), new DateTime(2021, 4, 15), 0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(3).Result,
                    (new Stub()).GetGameById(4).Result,
                };

                //find no game
                yield return new object[]
                {
                    new DateTime(2021, 6, 15), null, 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new DateTime(2021, 6, 15), null, 0, 10, InitTarotDbManager(),
                };

                //find no game
                yield return new object[]
                {
                    null, new DateTime(2020, 6, 15), 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    null, new DateTime(2020, 6, 15), 0, 10, InitTarotDbManager(),
                };

                //find no game
                yield return new object[]
                {
                    new DateTime(2019, 6, 15), new DateTime(2020, 6, 15), 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new DateTime(2019, 6, 15), new DateTime(2020, 6, 15), 0, 10, InitTarotDbManager(),
                };

                //find no game
                yield return new object[]
                {
                    new DateTime(2021, 6, 15), new DateTime(2022, 6, 15), 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new DateTime(2021, 6, 15), new DateTime(2022, 6, 15), 0, 10, InitTarotDbManager(),
                };

                //wrong dates
                yield return new object[]
                {
                    new DateTime(2021, 6, 15), new DateTime(2010, 6, 15), 0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new DateTime(2021, 6, 15), new DateTime(2010, 6, 15), 0, 10, InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> GetGamesBySession
        {
            get
            {
                //find 1 game
                yield return new object[]
                {
                    new Session("youpi", null, null,
                        (new Stub()).GetPlayerById(10).Result,
                        (new Stub()).GetPlayerById(8).Result,
                        (new Stub()).GetPlayerById(12).Result,
                        (new Stub()).GetPlayerById(9).Result,
                        (new Stub()).GetPlayerById(11).Result),
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(3).Result,
                };
                yield return new object[]
                {
                    new Session("youpi", null, null,
                        (new Stub()).GetPlayerById(10).Result,
                        (new Stub()).GetPlayerById(8).Result,
                        (new Stub()).GetPlayerById(12).Result,
                        (new Stub()).GetPlayerById(9).Result,
                        (new Stub()).GetPlayerById(11).Result),
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(3).Result,
                };

                //find 0 game, wrong dates
                yield return new object[]
                {
                    new Session("youpi", new DateTime(2021, 6, 7), null,
                        (new Stub()).GetPlayerById(8).Result,
                        (new Stub()).GetPlayerById(9).Result,
                        (new Stub()).GetPlayerById(10).Result,
                        (new Stub()).GetPlayerById(11).Result,
                        (new Stub()).GetPlayerById(12).Result),
                    0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new Session("youpi", new DateTime(2021, 6, 7), null,
                        (new Stub()).GetPlayerById(8).Result,
                        (new Stub()).GetPlayerById(9).Result,
                        (new Stub()).GetPlayerById(10).Result,
                        (new Stub()).GetPlayerById(11).Result,
                        (new Stub()).GetPlayerById(12).Result),
                    0, 10, InitTarotDbManager(),
                };

                //find 0 game, missing one player
                yield return new object[]
                {
                    new Session("youpi", null, null,
                        (new Stub()).GetPlayerById(9).Result,
                        (new Stub()).GetPlayerById(8).Result,
                        (new Stub()).GetPlayerById(11).Result,
                        (new Stub()).GetPlayerById(12).Result),
                    0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new Session("youpi", null, null,
                        (new Stub()).GetPlayerById(9).Result,
                        (new Stub()).GetPlayerById(8).Result,
                        (new Stub()).GetPlayerById(11).Result,
                        (new Stub()).GetPlayerById(12).Result),
                    0, 10, InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> GetGamesByPlayer
        {
            get
            {
                //find 2 games
                yield return new object[]
                {
                    (new Stub()).GetPlayerById(2).Result,
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    (new Stub()).GetPlayerById(2).Result,
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //find 1 game
                yield return new object[]
                {
                    (new Stub()).GetPlayerById(11).Result,
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(3).Result,
                };
                yield return new object[]
                {
                    (new Stub()).GetPlayerById(11).Result,
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(3).Result,
                };

                //find no game
                yield return new object[]
                {
                    (new Stub()).GetPlayerById(15).Result,
                    0, 10, new Stub(),
                };
                yield return new object[]
                {
                    (new Stub()).GetPlayerById(15).Result,
                    0, 10, InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> GetGamesByPlayers
        {
            get
            {
                //1 player => 2 games
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(2).Result },
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(2).Result },
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //1 player =>  1 game
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(11).Result },
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(3).Result,
                };
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(11).Result },
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(3).Result,
                };

                //1 player =>  no game
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(15).Result },
                    0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(15).Result },
                    0, 10, InitTarotDbManager(),
                };

                //2 players => 2 games
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(4).Result,
                                   (new Stub()).GetPlayerById(7).Result },
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(5).Result,
                };
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(4).Result,
                                   (new Stub()).GetPlayerById(7).Result },
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(2).Result,
                    (new Stub()).GetGameById(5).Result,
                };

                //2 players => 1 game
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(1).Result,
                                   (new Stub()).GetPlayerById(3).Result },
                    0, 10, new Stub(),
                    (new Stub()).GetGameById(1).Result,
                };
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(1).Result,
                                   (new Stub()).GetPlayerById(3).Result },
                    0, 10, InitTarotDbManager(),
                    (new Stub()).GetGameById(1).Result,
                };

                //2 players => 0 game
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(1).Result,
                                   (new Stub()).GetPlayerById(10).Result },
                    0, 10, new Stub(),
                };
                yield return new object[]
                {
                    new Player[] { (new Stub()).GetPlayerById(1).Result,
                                   (new Stub()).GetPlayerById(10).Result },
                    0, 10, InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> AddGame
        {
            get
            {
                //3 players game
                yield return new object[]
                {
                    true, new Stub(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    0, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };
                yield return new object[]
                {
                    true, InitTarotDbManager(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    0, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };

                //3 players game, but with existing id
                yield return new object[]
                {
                    false, new Stub(), 3,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    3, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };
                yield return new object[]
                {
                    false, InitTarotDbManager(), 3,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    3, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };

                //3 players game, but with id different from 0
                yield return new object[]
                {
                    false, new Stub(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    6, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };
                yield return new object[]
                {
                    false, InitTarotDbManager(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    6, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };

                //4 players game but with same player
                yield return new object[]
                {
                    true, new Stub(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    0, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };
                yield return new object[]
                {
                    true, InitTarotDbManager(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    0, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };

                //4 players game but with one new player
                yield return new object[]
                {
                    true, new Stub(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                        Tuple.Create(new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    0, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create(new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };
                yield return new object[]
                {
                    true, InitTarotDbManager(), 6,
                    new Tuple<Player, Model.Bidding>[]
                    {
                        Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                        Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create(new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), Model.Bidding.Opponent),
                        Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                    },
                    0, new DateTime(2022, 1, 3), RulesFactory.Create(RulesFactory.RulesNames.First()),
                    46, Model.PetitResult.SavedAuBout, Model.Poignée.None,
                    true, false, Model.Chelem.Unknown,
                    Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.Garde),
                    Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                    Tuple.Create(new Player("Bojan", "Zulfikarpasic", "Bojan Z", null), Model.Bidding.Opponent),
                    Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)
                };
            }
        }

        public static IEnumerable<object[]> DeleteGame
        {
            get
            {
                //delete valid game
                yield return new object[]
                {
                    true,
                    (new Stub()).GetGameById(1).Result,
                    new Stub(),
                };
                yield return new object[]
                {
                    true,
                    (new Stub()).GetGameById(1).Result,
                    InitTarotDbManager(),
                };
                //delete valid game
                yield return new object[]
                {
                    true,
                    (new Stub()).GetGameById(4).Result,
                    new Stub(),
                };
                yield return new object[]
                {
                    true,
                    (new Stub()).GetGameById(4).Result,
                    InitTarotDbManager(),
                };

                //delete invalid game
                yield return new object[]
                {
                    false,
                    new Game(6, new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, true, false, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.GardeSans),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)),
                    new Stub(),
                };
                yield return new object[]
                {
                    false,
                    new Game(6, new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, true, false, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.GardeSans),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)),
                    InitTarotDbManager(),
                };

                //delete invalid game
                yield return new object[]
                {
                    false,
                    new Game(new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, true, false, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.GardeSans),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)),
                    new Stub(),
                };
                yield return new object[]
                {
                    false,
                    new Game(new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, Model.PetitResult.SavedAuBout, Model.Poignée.Simple, true, false, Model.Chelem.Unknown,
                            Tuple.Create((new Stub()).GetPlayerById(1).Result, Model.Bidding.GardeSans),
                            Tuple.Create((new Stub()).GetPlayerById(2).Result, Model.Bidding.Opponent),
                            Tuple.Create((new Stub()).GetPlayerById(3).Result, Model.Bidding.Opponent)),
                    InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> DeleteGameById
        {
            get
            {
                //delete valid game
                yield return new object[]
                {
                    true, 1, new Stub(),
                };
                yield return new object[]
                {
                    true, 3, InitTarotDbManager(),
                };

                //delete valid game
                yield return new object[]
                {
                    true, 3, new Stub(),
                };
                yield return new object[]
                {
                    true, 3, InitTarotDbManager(),
                };

                //delete invalid game
                yield return new object[]
                {
                    false, 0, new Stub(),
                };
                yield return new object[]
                {
                    false, 0, InitTarotDbManager(),
                };

                //delete invalid game
                yield return new object[]
                {
                    false, 6, new Stub(),
                };
                yield return new object[]
                {
                    false, 6, InitTarotDbManager(),
                };
            }
        }

        public static IEnumerable<object[]> UpdateGame
        {
            get
            {
                //update valid game
                yield return new object[]
                {
                    true, 1,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    new Stub(),
                };

                yield return new object[]
                {
                    true, 1,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    InitTarotDbManager(),
                };
                //update valid game
                yield return new object[]
                {
                    true, 5,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    new Stub(),
                };

                yield return new object[]
                {
                    true, 5,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    InitTarotDbManager(),
                };

                //update valid game
                yield return new object[]
                {
                    true, 5,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail),
                    new Stub(),
                };
                yield return new object[]
                {
                    true, 5,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail),
                    InitTarotDbManager(),
                };

                //invalid update game
                yield return new object[]
                {
                    false, 0,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    new Stub(),
                };

                yield return new object[]
                {
                    false, 0,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    InitTarotDbManager(),
                };
                //invalid update game
                yield return new object[]
                {
                    false, 6,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    new Stub(),
                };

                yield return new object[]
                {
                    false, 6,
                    new Game(new DateTime(2022, 1, 3), new FrenchTarotRules(), 32, Model.PetitResult.Lost, Model.Poignée.Double, false, true, Model.Chelem.Fail,
                            Tuple.Create(new Stub().GetPlayerById(6).Result, Model.Bidding.Pousse),
                            Tuple.Create(new Stub().GetPlayerById(7).Result, Model.Bidding.KingCalled),
                            Tuple.Create(new Stub().GetPlayerById(12).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(5).Result, Model.Bidding.Opponent),
                            Tuple.Create(new Stub().GetPlayerById(8).Result, Model.Bidding.Opponent)),
                    InitTarotDbManager(),
                };
            }
        }
    }
}
