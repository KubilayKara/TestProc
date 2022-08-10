using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using Scorer;
using System.Collections.Generic;

namespace ScorerTests
{
    [TestFixture]
    public class GameProcessorTest
    {
        [Test]
        public void LoadGames_ShouldReturnGames()
        {


            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDataAccess>()
                    .Setup(x => x.LoadData<GameModel>("SELECT * FROM GAMES"))
                    .Returns(GetSampleGames());
                var cls = mock.Create<GameProcessor>();

                List<GameModel> expected = GetSampleGames();

                List<GameModel> actual = cls.LoadGames();

                Assert.True(actual != null);
                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        [Test]
        public void UpdateGame_ShouldCallSaveData()
        {
            string sql = "UPDATE GAMES ...";


            using (var mock = AutoMock.GetLoose())
            {

                var game = GetSampleGames()[0];
                mock.Mock<IDataAccess>()
                    .Setup(x => x.SaveData(game, sql));

                var cls = mock.Create<GameProcessor>();
                cls.UpdateGame(game);

                mock.Mock<IDataAccess>().Verify(x => x.SaveData(game, sql), Times.Exactly(1));
            }
        }

        private List<GameModel> GetSampleGames()
        {
            var result = new List<GameModel> { new GameModel{Id=1, Name="Game 1" },
                new GameModel{Id=2, Name="Game 2" },
                new GameModel{Id=3, Name="Game 3" } };

            return result;
        }
    }
}