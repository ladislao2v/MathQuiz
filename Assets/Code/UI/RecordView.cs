using System.Collections.Generic;
using Code.Services.RecordService;
using Code.Services.ScoreService;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private GameInfoView _gameInfoViewPrefab;
        [SerializeField] private Transform _container;

        private Dictionary<int, string> _levelNames = new()
        {
            [0] = "Bronze league",
            [1] = "Silver league",
            [2] = "Gold league",
        };

        private List<GameInfoView> _active = new();
        private IRecordService _recordService;

        [Inject]
        private void Construct(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public void TurnOn()
        {
            if(_recordService.Results.Count == 0)
                return;
            
            if (_active.Count == 0)
                GenerateViews(_recordService.Results.Count);
            else if(_active.Count < _recordService.Results.Count)
                GenerateViews(_recordService.Results.Count-_active.Count);
            
            InitializeViews(_recordService.Results.Count);
        }

        private void GenerateViews(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var view = Instantiate(_gameInfoViewPrefab, _container);
                view.gameObject.SetActive(false);
                _active.Add(view);
            }
        }

        private void InitializeViews(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _active[i].Construct(i + 1,
                    _levelNames[_recordService.Results[i].Level - 1],
                    _recordService.Results[i].PlayerScore,
                    _recordService.Results[i].EnemyScore);
                
                _active[i].gameObject.SetActive(true);
            }
        }

        public void TurnOff()
        {
            foreach (var view in _active)
            {
                view.gameObject.SetActive(false);
            }
        }
    }
}