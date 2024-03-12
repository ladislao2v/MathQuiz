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
    public class StatsOverlay : Overlay
    {
        [SerializeField] private TextMeshProUGUI _correctAnswersText;
        [SerializeField] private TextMeshProUGUI _incorrectAnswersText;
        [SerializeField] private RecordView _recordView;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private Button _backButton;
        
        private IStatsService _statsService;
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine, IStatsService statsService)
        {
            _stateMachine = stateMachine;
            _statsService = statsService;
        }
        
        private void OnEnable()
        {
            _correctAnswersText.text = _statsService.CorrectAnswers.ToString();
            _incorrectAnswersText.text = _statsService.IncorrectAnswers.ToString();
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