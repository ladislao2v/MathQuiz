using Code.Services.AudioService;
using Code.Services.GameDataService;
using Code.Services.LevelSelectorService;
using Code.Services.PauseService;
using Code.Services.RecordService;
using Code.Services.ScoreService;
using Code.Services.StatsService;
using Code.Services.TimerService;

namespace Code.StateMachine.States
{
    public class InitialState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameDataService _gameDataService;
        private readonly IPauseService _pauseService;
        private readonly IRecordService _recordService;
        private readonly ITimer _timer;
        private readonly ILevelSelector _levelSelector;
        private readonly IStatsService _statsService;
        private readonly IAudioService _audioService;

        public InitialState(IStateMachine stateMachine,
            IGameDataService gameDataService, 
            IPauseService pauseService,
            IRecordService recordService,
            ITimer timer,
            ILevelSelector levelSelector,
            IStatsService statsService,
            IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
            _pauseService = pauseService;
            _recordService = recordService;
            _timer = timer;
            _levelSelector = levelSelector;
            _statsService = statsService;
            _audioService = audioService;
        }

        public void Enter()
        {
            InitializeGameDataService();
            InitializePauseService();

            _stateMachine.Enter<LoadDataState>();
        }

        private void InitializeGameDataService()
        {
            
            _gameDataService.Add(_statsService);
            _gameDataService.Add(_recordService);
            _gameDataService.Add(_levelSelector);
            _gameDataService.Add(_audioService);
            _gameDataService.Add(_recordService);
        }

        private void InitializePauseService()
        {
            _pauseService.Add(_timer);
        }


        public void Exit() { }
    }
}