using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Xunit;

namespace Model_UT
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
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 3, false,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //empty name
                yield return new object[]
                {
                    42, "no name", new DateTime(1977, 5, 27), null, 3, false,
                    42, "", new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //white name
                yield return new object[]
                {
                    42, "no name", new DateTime(1977, 5, 27), null, 3, false,
                    42, "    ", new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //null name
                yield return new object[]
                {
                    42, "no name", new DateTime(1977, 5, 27), null, 3, false,
                    42, null, new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //no players
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 0, false,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                };
                //1 player
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 1, false,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                };
                //2 identical players
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 3, false,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Stan", "Getz", "", null)
                };
                //2 valid dates
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1981, 6, 25), 3, false,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1981, 6, 25), 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //1 valid date and no starting date
                yield return new object[]
                {
                    42, "les semi-croustillants", null, new DateTime(1981, 6, 25), 3, false,
                    42, "les semi-croustillants", null, new DateTime(1981, 6, 25), 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //1 valid date and no ending date
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 3, false,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //2 invalid dates (equal)
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1977, 5, 27), 3, true,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1977, 5, 27), 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
                //2 invalid dates (starting after ending)
                yield return new object[]
                {
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1975, 5, 27), 3, true,
                    42, "les semi-croustillants", new DateTime(1977, 5, 27), new DateTime(1975, 5, 27), 
                    new Player("Charlie", "Parker", "Bird", null),
                    new Player("Dizzy", "Gillespie", "Dizz", null),
                    new Player("Stan", "Getz", "", null)
                };
            }
        }

        public static IEnumerable<object[]> AddPlayer
        {
            get
            {
                //add new Player
                yield return new object[]
                {
                    true, 4,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Chick", "Corea", "", null)
                };
                //add existing Player
                yield return new object[]
                {
                    false, 3,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Dizzy", "Gillespie", "Dizz", null)
                };
                //add null Player
                yield return new object[]
                {
                    false, 3,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    null
                };
            }
        }
        public static IEnumerable<object[]> AddPlayers
        {
            get
            {
                //add 2 new Players
                yield return new object[]
                {
                    true, 5,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Chick", "Corea", "", null),
                    new Player("Jean-Luc", "Ponty", "", null)
                };
                //add 1 new Player
                yield return new object[]
                {
                    true, 4,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Chick", "Corea", "", null),
                };
                //add no Player
                yield return new object[]
                {
                    false, 3,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                };
                //add twice the same Player and another one
                yield return new object[]
                {
                    true, 5,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Chick", "Corea", "", null),
                    new Player("Chick", "Corea", "", null),
                    new Player("Jean-Luc", "Ponty", "", null)
                };
                //add 1 new Player and an existing one
                yield return new object[]
                {
                    true, 4,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Chick", "Corea", "", null),
                    new Player("Stan", "Getz", "", null)
                };
            }
        }

        public static IEnumerable<object[]> RemovePlayer
        {
            get
            {
                //remove existing Player
                yield return new object[]
                {
                    true, 2,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Dizzy", "Gillespie", "Dizz", null)
                };
                //remove non existing Player
                yield return new object[]
                {
                    false, 3,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    new Player("Chick", "Corea", "Dizz", null)
                };
                //remove null Player
                yield return new object[]
                {
                    false, 3,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                    null
                };
                //remove Player on an empty session
                yield return new object[]
                {
                    false, 0,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null),
                    new Player("Dizzy", "Gillespie", "Dizz", null)
                };
                //remove Player on a session with one player
                yield return new object[]
                {
                    true, 0,
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null,
                                new Player("Dizzy", "Gillespie", "Dizz", null)),
                    new Player("Dizzy", "Gillespie", "Dizz", null)
                };
            }
        }

        public static IEnumerable<object[]> ClearPlayers
        {
            get
            {
                //classic session
                yield return new object[]
                {
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null, 
                        new Player("Charlie", "Parker", "Bird", null),
                        new Player("Dizzy", "Gillespie", "Dizz", null),
                        new Player("Stan", "Getz", "", null)),
                };
                //empty session
                yield return new object[]
                {
                    new Session(42, "les semi-croustillants", new DateTime(1977, 5, 27), null), 
                };
            }
        }
    }

    

    public class Session_UT
    {
        [Theory]
        [MemberData(nameof(TestData_Session.Sessions), MemberType = typeof(TestData_Session))]
        public void TestConstructor(long expectedId, string expectedName, DateTime? expectedStart,
            DateTime? expectedEnd, int expectedNbPlayers, bool shouldThrowException,
            long id, string name, DateTime? start, DateTime? end, params Player[] players)
        {
            if(shouldThrowException)
            {
                Assert.Throws<ArgumentException>(() => new Session(id, name, start, end, players));
                Assert.Throws<ArgumentException>(() => new Session(name, start, end, players));
                return;
            }
            Session session = new Session(id, name, start, end, players);
            Session session2 = new Session(name, start, end, players);
            Assert.Equal(expectedId, session.Id);
            Assert.Equal(0, session2.Id);
            Assert.Equal(expectedName, session.Name);
            Assert.Equal(expectedName, session2.Name);
            Assert.Equal(expectedStart, session.StartingTime);
            Assert.Equal(expectedStart, session2.StartingTime);
            Assert.Equal(expectedEnd, session.EndingTime);
            Assert.Equal(expectedEnd, session2.EndingTime);
            Assert.Equal(expectedNbPlayers, session.Players.Count);
            Assert.Equal(expectedNbPlayers, session2.Players.Count);
            foreach(Player p in players)
            {
                Assert.Contains(p, session.Players);
                Assert.Contains(p, session2.Players);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Session.AddPlayer), MemberType = typeof(TestData_Session))]
        public void TestAddPlayer(bool expectedReturn, int expectedNbPlayers, Session session, Player playerToAdd)
        {
            bool result = session.AddPlayer(playerToAdd);
            Assert.Equal(expectedReturn, result);
            Assert.Equal(expectedNbPlayers, session.Players.Count);
            if(result)
            {
                Assert.Equal(playerToAdd, session.Players.Last());
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Session.AddPlayers), MemberType = typeof(TestData_Session))]
        public void TestAddPlayers(bool expectedReturn, int expectedNbPlayers, Session session, params Player[] playersToAdd)
        {
            bool result = session.AddPlayers(playersToAdd);
            Assert.Equal(expectedReturn, result);
            Assert.Equal(expectedNbPlayers, session.Players.Count);
            if(result)
            {
                foreach(var player in playersToAdd)
                    Assert.Contains(player, session.Players);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Session.RemovePlayer), MemberType = typeof(TestData_Session))]
        public void TestRemovePlayer(bool expectedReturn, int expectedNbPlayers, Session session, Player playerToRemove)
        {
            bool result = session.RemovePlayer(playerToRemove);
            Assert.Equal(expectedReturn, result);
            Assert.Equal(expectedNbPlayers, session.Players.Count);
            if(result)
            {
                Assert.DoesNotContain(playerToRemove, session.Players);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Session.ClearPlayers), MemberType = typeof(TestData_Session))]
        public void TestClearPlayers(Session session)
        {
            session.ClearPlayers();
            Assert.Empty(session.Players);
        }
    }
}
