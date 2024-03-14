using Code.Services.LevelSelectorService;
using Code.Services.StaticDataService;
using Code.Services.StaticDataService.Configs;
using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Menu
{
    public class MenuOverlay : Overlay
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _recordButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _policyButton;
        [SerializeField] private LevelMap _levelMap;
        [SerializeField] private LevelSelectorView _levelSelectorView;

        private ILevelSelector _levelSelector;
        private IStateMachine _stateMachine;

        [Inject]
        public void Construct(IStateMachine stateMachine,
            ILevelSelector levelSelector)
        {
            _stateMachine = stateMachine;
            _levelSelector = levelSelector;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _recordButton.onClick.AddListener(OnRecordButton);
            _policyButton.onClick.AddListener(OnPolicyButton);
            _optionButton.onClick.AddListener(OnOptionButton);
            _levelSelectorView.TurnOn();

            for(int i = 1; i <= _levelSelector.LevelCount; i++)
                ChangeLevel(i);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _recordButton.onClick.RemoveListener(OnRecordButton);
            _policyButton.onClick.RemoveListener(OnPolicyButton);
            _optionButton.onClick.RemoveListener(OnOptionButton);
            _levelSelectorView.TurnOff();
        }

        private void OnPlayButtonClicked()
        {
            _levelMap.Show();
        }

        private void ChangeLevel(int current)
        {
            _levelSelectorView.SetCurrentLevel(current, _levelSelector.IsOpen(current));
        }

        private void OnRecordButton()
        {
            _stateMachine.Enter<RecordState>();
        }
        
        private void OnPolicyButton()
        {
            _stateMachine.Enter<PolicyState>();
        }
        
        private void OnOptionButton()
        {
            _stateMachine.Enter<OptionState>();
        }
    }
}