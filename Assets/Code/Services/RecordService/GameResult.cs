using System;

namespace Code.Services.RecordService
{
    [Serializable]
    public class GameResult : IGameResult
    {
        public int Level { get; }
        public int PlayerScore { get; }
        public int EnemyScore { get; }

        public GameResult(int level, int playerScore, int enemyScore)
        {
            Level = level;
            PlayerScore = playerScore;
            EnemyScore = enemyScore;
        }
    }
}