using Code.Services.LevelSelectorService;
using Code.Services.StaticDataService;
using Code.Services.StaticDataService.Configs;
using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.UI.Menu
{
    public class MenuOverlay : Overlay
    {
        [SerializeField] private LevelSelectorView _levelSelectorView;
        //[SerializeField] private Button _playButton;

        private IStaticDataService _staticDataService;
        private ILevelSelector _levelSelector;
        private IStateMachine _stateMachine;

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
            //_playButton.onClick.AddListener(OnPlayButtonClicked);
            _levelSelectorView.TurnOn();

            for(int i = 1; i <= _levelSelector.LevelCount; i++)
                ChangeLevel(i);
        }

        private void OnDisable()
        {
            //_playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _levelSelectorView.TurnOff();
        }

        private void OnPlayButtonClicked()
        {
            
        }

        private void ChangeLevel(int current)
        {
            //LevelConfig levelConfig = _staticDataService.GetChampionship(current);
            _levelSelectorView.SetCurrentLevel(current, _levelSelector.IsOpen(current));
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