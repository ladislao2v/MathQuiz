using Code.Services.StatsService;
using Code.StateMachine;
using Code.StateMachine.States;
using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Discription
{
    public class DiscriptionOverlay : Overlay
    {
        [SerializeField] private RecordView _recordView;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private Button _backButton;
        
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _recordView.TurnOn();
            _soundButton.TurnOn();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _recordView.TurnOff();
            _soundButton.TurnOff();
        }

        private void OnBackButtonClicked()
        {
            _stateMachine.Enter<SaveDataState>();
        }
    }
}