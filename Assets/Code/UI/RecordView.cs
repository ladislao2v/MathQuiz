using System.Collections.Generic;
using Code.Services.RecordService;
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
            [1] = "Bronze league",
            [2] = "Silver league",
            [3] = "Gold league",
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
            int count = 0;
            
            foreach (var gameResult in _recordService.Results)
            {
                if (gameResult != null)
                    count++;
            }
            
            GenerateViews(count);
            
            InitializeViews(count);
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
                    _levelNames[_recordService.Results[i].Level],
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