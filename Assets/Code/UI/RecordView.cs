using Code.Services.RecordService;
using Code.Services.ScoreService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordText;
        
        private IRecordService _recordService;

        [Inject]
        private void Construct(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public void TurnOn()
        {
            //_recordText.text = _recordService.Record.ToString();
        }

        public void TurnOff()
        {
            //_recordText.text = _recordService.Record.ToString();
        }
    }
}