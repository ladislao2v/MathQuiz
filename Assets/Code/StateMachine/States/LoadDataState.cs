using Code.Services.AudioService;
using Code.Services.GameDataService;
using Code.Services.LevelSelectorService;
using Code.Services.RecordService;
using Code.Services.ScoreService;
using Code.Services.StatsService;
using UnityEngine;

namespace Code.StateMachine.States
{
    public sealed class LoadDataState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameDataService _gameDataService;
        private readonly IRecordService _recordService;
        private readonly ILevelSelector _levelSelector;
        private readonly IStatsService _statsService;
        private readonly IAudioService _audioService;

        public LoadDataState(IStateMachine stateMachine, 
            IGameDataService gameDataService, 
            IRecordService recordService,
            ILevelSelector levelSelector,
            IStatsService statsService,
            IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
            _recordService = recordService;
            _levelSelector = levelSelector;
            _statsService = statsService;
            _audioService = audioService;
        }

        public void Enter()
        {
            Load();
            
            _stateMachine.Enter<MenuState>();
        }

        private void Load()
        {
            _gameDataService.Add(_statsService);
            _gameDataService.Add(_recordService);
            _gameDataService.Add(_levelSelector);
            _gameDataService.Add(_audioService);
            _gameDataService.LoadData();
        }

        public void Exit() { }
    }
}