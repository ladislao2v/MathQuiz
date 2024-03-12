using Code.Services.ScoreService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordText;
        
        private IScoreService _scoreService;

        [Inject]
        private void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void TurnOn()
        {
            _recordText.text = _scoreService.Record.ToString();
        }

        public void TurnOff()
        {
            _recordText.text = _scoreService.Record.ToString();
        }
    }
}