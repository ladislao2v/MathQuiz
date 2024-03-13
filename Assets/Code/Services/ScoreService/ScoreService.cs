using System;
using Code.Services.RecordService;
using Code.Services.SaveLoadDataService;

namespace Code.Services.ScoreService
{
    public class ScoreService : IScoreService
    {
        private readonly IRecordService _recordService;
        
        private int _playerScore;
        private int _enemyScore;
        
        public event Action<int> PlayerScoreChanged;
        public event Action<int> EnemyScoreChanged;
        
        public ScoreService(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public void AddPlayerScore(int points)
        {
            if (points < 0)
                throw new ArgumentOutOfRangeException(nameof(points));

            _playerScore += points;
            
            PlayerScoreChanged?.Invoke(_playerScore);
        }

        public void AddEnemyScore(int points)
        {
            if (points < 0)
                throw new ArgumentOutOfRangeException(nameof(points));

            _enemyScore += points;
            
            EnemyScoreChanged?.Invoke(_enemyScore);
        }

        public void Reset()
        {
            IGameResult gameResult = 
                new GameResult(_playerScore, _enemyScore);
            
            _recordService.Save(gameResult);
            
            _enemyScore = 0;
            _playerScore = 0;
            
            
            PlayerScoreChanged?.Invoke(_playerScore);
            EnemyScoreChanged?.Invoke(_enemyScore);
        }
    }
}