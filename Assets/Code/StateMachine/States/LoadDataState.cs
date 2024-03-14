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
        public LoadDataState(IStateMachine stateMachine, 
            IGameDataService gameDataService)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
        }

        public void Enter()
        {
            Load();
            
            _stateMachine.Enter<MenuState>();
        }

        private void Load()
        {
            _gameDataService.LoadData();
        }

        public void Exit() { }
    }
}