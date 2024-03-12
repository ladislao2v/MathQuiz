using Code.Services.LevelSelectorService;
using Code.Services.StaticDataService;
using Code.Services.StaticDataService.Configs;
using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Menu
{
    public class MenuOverlay : Overlay
    {
        [SerializeField] private LevelSelectorView _levelSelectorView;
        [FormerlySerializedAs("_championshipDataView")] [SerializeField] private LevelView levelView;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statsButton;
        [SerializeField] private Button _discriptionButton;
        [SerializeField] private RecordView _recordView;
        [SerializeField] private SoundButton _soundButton;

        private IStaticDataService _staticDataService;
        private ILevelSelector _levelSelector;
        private IStateMachine _stateMachine;
        private int _currentLevel = 1;

        [Inject]
        public void Construct(IStateMachine stateMachine, 
            IStaticDataService staticDataService, 
            ILevelSelector levelSelector)
        {
            _stateMachine = stateMachine;
            _levelSelector = levelSelector;
            _staticDataService = staticDataService;
        }

        private void OnEnable()
        {
            _levelSelectorView.LeftButton.onClick.AddListener(Previous);
            _levelSelectorView.RightButton.onClick.AddListener(Next);
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _statsButton.onClick.AddListener(OnStatsButton);
            _discriptionButton.onClick.AddListener(OnDiscriptionButton);
            _recordView.TurnOn();
            _soundButton.TurnOn();
        }

        private void OnDisable()
        {
            _levelSelectorView.LeftButton.onClick.RemoveListener(Previous);
            _levelSelectorView.RightButton.onClick.RemoveListener(Next);
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _statsButton.onClick.RemoveListener(OnStatsButton);
            _discriptionButton.onClick.RemoveListener(OnDiscriptionButton);
            _recordView.TurnOff();
            _soundButton.TurnOff();
        }

        private void Next()
        {
            _currentLevel++;

            if (_currentLevel == 5)
            {
                _currentLevel = 1;
            }

            ChangeLevel();
        }

        private void Previous()
        {
            _currentLevel--;

            if (_currentLevel == 0)
            {
                _currentLevel = 4;
            }
            
            ChangeLevel();
        }

        private void OnPlayButtonClicked()
        {
            _levelSelector.Select(_currentLevel);
            _stateMachine.Enter<QuestionsState>();
        }

        private void ChangeLevel()
        {
            LevelConfig levelConfig = _staticDataService.GetChampionship(_currentLevel);
            _levelSelectorView.SetCurrentLevel(_currentLevel, _levelSelector.IsOpen(_currentLevel));
            levelView.SetData(levelConfig);
        }

        private void OnStatsButton()
        {
            _stateMachine.Enter<StatsState>();
        }
        
        private void OnDiscriptionButton()
        {
            _stateMachine.Enter<DiscriptionState>();
        }
    }
}