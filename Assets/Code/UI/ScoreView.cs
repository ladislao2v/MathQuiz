using Code.Services.ScoreService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private TextMeshProUGUI _enemyScoreText;
        
        private IScoreService _scoreService;

        [Inject]
        private void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Reset()
        {
            _playerScoreText.text = 0.ToString();
            _enemyScoreText.text = 0.ToString();
        }

        public void TurnOn()
        {
            _scoreService.PlayerScoreChanged += OnPlayerScoreChanged;
            _scoreService.EnemyScoreChanged += OnEnemyScoreChanged;
        }

        public void TurnOff()
        {
            _scoreService.PlayerScoreChanged -= OnPlayerScoreChanged;
            _scoreService.EnemyScoreChanged -= OnEnemyScoreChanged;
        }

        private void OnEnemyScoreChanged(int score)
        {
            _enemyScoreText.text = score.ToString();
        }

        private void OnPlayerScoreChanged(int score)
        {
            _playerScoreText.text = score.ToString();
        }
    }
}