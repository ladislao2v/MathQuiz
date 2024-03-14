using System;
using Code.Services.LevelSelectorService;
using Code.Services.RecordService;
using Code.Services.SaveLoadDataService;

namespace Code.Services.ScoreService
{
    public class ScoreService : IScoreService
    {
        private readonly IRecordService _recordService;
        private readonly ILevelSelector _levelSelector;

        private int _playerScore;
        private int _enemyScore;
        
        public event Action<int> PlayerScoreChanged;
        public event Action<int> EnemyScoreChanged;
        
        public ScoreService(IRecordService recordService, ILevelSelector levelSelector)
        {
            _recordService = recordService;
            _levelSelector = levelSelector;
        }

        public void AddPlayerScore(int points = 1)
        {
            if (points < 0)
                throw new ArgumentOutOfRangeException(nameof(points));

            _playerScore += points;
            
            PlayerScoreChanged?.Invoke(_playerScore);
        }

        public void AddEnemyScore(int points = 1)
        {
            if (points < 0)
                throw new ArgumentOutOfRangeException(nameof(points));

            _enemyScore += points;
            
            EnemyScoreChanged?.Invoke(_enemyScore);
        }

        public void Reset()
        {
            IGameResult gameResult = 
                new GameResult(_levelSelector.SelectedLevel, _playerScore, _enemyScore);
            
            _recordService.Save(gameResult);
            
            _enemyScore = 0;
            _playerScore = 0;
            
            
            PlayerScoreChanged?.Invoke(_playerScore);
            EnemyScoreChanged?.Invoke(_enemyScore);
        }

        public bool IsPlayerWin()
        {
            if (_playerScore == _enemyScore)
                AddPlayerScore();
            
            PlayerScoreChanged?.Invoke(_playerScore);
            EnemyScoreChanged?.Invoke(_enemyScore);

            var isWin = _playerScore > _enemyScore;
            
            Reset();
            return isWin;
        }
    }
}