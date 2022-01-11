using System;
using Model;
using Xunit;

namespace Model_UT
{
    public class Player_UT
    {
        [Theory]
        [InlineData(true, 42, "Thomas Wright", "Waller", "Fats", "fats.jpg")]
        [InlineData(true, 42, "", "Waller", "Fats", "fats.jpg")]
        [InlineData(true, 42, "  ", "Waller", "Fats", "fats.jpg")]
        [InlineData(true, 42, null, "Waller", "Fats", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", "", "Fats", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", "   ", "Fats", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", null, "Fats", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", "Waller", "", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", "Waller", "  ", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", "Waller", null, "fats.jpg")]
        [InlineData(true, 42, "", "", "Fats", "fats.jpg")]
        [InlineData(true, 42, null, "", "Fats", "fats.jpg")]
        [InlineData(true, 42, "", null, "Fats", "fats.jpg")]
        [InlineData(true, 42, "Thomas Wright", "", "", "fats.jpg")]
        [InlineData(true, 42, "", "Waller", "", "fats.jpg")]
        [InlineData(true, 42, null, "Waller", null, "fats.jpg")]
        [InlineData(false, 42, null, null, null, "fats.jpg")]
        [InlineData(false, 42, "", null, null, "fats.jpg")]
        [InlineData(false, 42, "  ", null, null, "fats.jpg")]
        [InlineData(false, 42, null, "", null, "fats.jpg")]
        [InlineData(false, 42, "", "", null, "fats.jpg")]
        [InlineData(false, 42, "  ", "", null, "fats.jpg")]
        [InlineData(false, 42, null, null, "", "fats.jpg")]
        [InlineData(false, 42, "", null, "", "fats.jpg")]
        [InlineData(false, 42, "  ", null, "", "fats.jpg")]
        [InlineData(false, 42, null, "", "", "fats.jpg")]
        [InlineData(false, 42, "", "", "", "fats.jpg")]
        [InlineData(false, 42, "  ", "", "", "fats.jpg")]
        public void TestConstructor(bool noException, long id, string firstname, string lastname, string nickname, string image)
        {
            if(!noException)
                Assert.Throws<ArgumentException>(() => new Player(id, firstname, lastname, nickname, image));
            else
            {
                Player p = new Player(id, firstname, lastname, nickname, image);
                Assert.NotNull(p);
                Assert.Equal(id, p.Id);
                Assert.Equal(string.IsNullOrWhiteSpace(firstname) ? "" : firstname, p.FirstName);
                Assert.Equal(string.IsNullOrWhiteSpace(lastname) ? "" : lastname, p.LastName);
                Assert.Equal(string.IsNullOrWhiteSpace(nickname) ? "" : nickname, p.NickName);
                Assert.Equal(string.IsNullOrWhiteSpace(image) ? null : image, p.Image);
            }
        }

        [Fact]
        public void TestConstructorId()
        {
            Player p = new Player("Thomas Wright", "Waller", "Fats", "fats.jpg");
            Assert.Equal(0, p.Id);
        }

        [Theory]
        [InlineData("Thomas Wright", "Thomas Wright")]
        [InlineData("", "")]
        [InlineData("", "  ")]
        [InlineData("", null)]
        public void TestFirstNameProperty(string expected, string firstname)
        {
            Player p = new Player(firstname, "Waller", "Fats", "fats.jpg");
            Assert.Equal(expected, p.FirstName);
        }

        [Theory]
        [InlineData("Waller", "Waller")]
        [InlineData("", "")]
        [InlineData("", "  ")]
        [InlineData("", null)]
        public void TestLastNameProperty(string expected, string lastname)
        {
            Player p = new Player("Thomas Wright", lastname, "Fats", "fats.jpg");
            Assert.Equal(expected, p.LastName);
        }

        [Theory]
        [InlineData("Fats", "Fats")]
        [InlineData("", "")]
        [InlineData("", "  ")]
        [InlineData("", null)]
        public void TestNickNameProperty(string expected, string nickname)
        {
            Player p = new Player("Thomas Wright", "Waller", nickname, "fats.jpg");
            Assert.Equal(expected, p.NickName);
        }

        [Theory]
        [InlineData("fats.jpg", "fats.jpg")]
        [InlineData(null, "")]
        [InlineData(null, "  ")]
        [InlineData(null, null)]
        public void TestImageProperty(string expected, string image)
        {
            Player p = new Player("Thomas Wright", "Waller", "Fats", image);
            Assert.Equal(expected, p.Image);
        }

        [Theory]
        [InlineData(true, 42, "Charlie", "Parker", "Bird", "bird.jpg",
                          42, "Charlie", "Parker", "Bird", "bird.jpg")]
        [InlineData(false, 0, "Charlie", "Parker", "Bird", "bird.jpg",
                           42, "Charlie", "Parker", "Bird", "bird.jpg")]
        [InlineData(false, 42, "Charlie", "Parker", "Bird", "bird.jpg",
                           0, "Charlie", "Parker", "Bird", "bird.jpg")]
        [InlineData(true, 42, "Charlie", "Parker", "Bird", "bird.jpg",
                           42, "Thomas Wright", "Waller", "Fats", "fats.jpg")]
        [InlineData(false, 0, "Charlie", "Parker", "Bird", "bird.jpg",
                           0, "Charles", "Parker", "Bird", "bird.jpg")]
        [InlineData(false, 0, "Charlie", "Parker", "Bird", "bird.jpg",
                           0, "Charlie", "", "Bird", "bird.jpg")]
        [InlineData(false, 0, "Charlie", "Parker", "Bird", "bird.jpg",
                           0, "Charlie", "Parker", "Birdie", "bird.jpg")]
        [InlineData(true, 0, "Charlie", "Parker", "Bird", "bird.jpg",
                          0, "Charlie", "Parker", "Bird", "charlie.jpg")]
        public void TestEqualityProtocol(bool expectedResult,
            long id1, string firstname1, string lastname1, string nickname1, string image1,
            long id2, string firstname2, string lastname2, string nickname2, string image2)
        {
            Player p1 = new Player(id1, firstname1, lastname1, nickname1, image1);
            Player p2 = new Player(id2, firstname2, lastname2, nickname2, image2);
            Assert.Equal(expectedResult, p1.Equals(p2));
        }
    }
}
