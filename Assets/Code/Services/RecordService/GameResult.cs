namespace Code.Services.RecordService
{
    public class GameResult : IGameResult
    {
        public int PlayerScore { get; }
        public int EnemyScore { get; }

        public GameResult(int playerScore, int enemyScore)
        {
            PlayerScore = playerScore;
            EnemyScore = enemyScore;
        }
    }
}