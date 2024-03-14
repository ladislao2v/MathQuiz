using System;
using Code.Services.StatsService;
using Code.StateMachine;
using Code.StateMachine.States;
using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Options
{
    public class OptionsOverlay : Overlay
    {
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private Button _backButton;
        
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine, IStatsService statsService)
        {
            _stateMachine = stateMachine;
        }
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _soundButton.TurnOn();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _soundButton.TurnOff();
        }

        private void OnBackButtonClicked()
        {
            _stateMachine.Enter<SaveDataState>();
        }
    }
}