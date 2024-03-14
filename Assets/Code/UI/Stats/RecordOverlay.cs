using System;
using Code.Services.StatsService;
using Code.StateMachine;
using Code.StateMachine.States;
using Code.UI.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Stats
{
    public class RecordOverlay : Overlay
    {
        [SerializeField] private RecordView _recordView;
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
            _recordView.TurnOn();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _recordView.TurnOff();
        }

        private void OnBackButtonClicked()
        {
            _stateMachine.Enter<SaveDataState>();
        }
    }
}