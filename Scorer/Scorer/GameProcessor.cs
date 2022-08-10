using System.Collections.Generic;

namespace Scorer
{
    public class GameProcessor
    {
        private readonly IDataAccess dataAccess;

        public GameProcessor(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public List<GameModel> LoadGames()
        {
            var result = this.dataAccess.LoadData<GameModel>("SELECT * FROM GAMES");

            return result;
        }

        public void UpdateGame(GameModel game)
        {
            string sql = "UPDATE GAMES ...";
            this.dataAccess.SaveData(game, sql);

        }
    }
}