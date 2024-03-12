﻿using System.Collections;
using Code.Extensions;
using Code.Services.AnswerCorrectnessService;
using Code.Services.CoroutineRunner;
using Code.Services.LevelSelectorService;
using Code.Services.ScoreService;
using Code.Services.StaticDataService.Configs;
using Code.Services.StatsService;
using Code.StateMachine;
using Code.StateMachine.States;
using Code.UI.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Gameplay
{
    public class GameplayOverlay : Overlay
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _backButton;
        [SerializeField] private VariantView _variantView;
        [SerializeField] private VariantButton[] _variantButtons;
        [SerializeField] private RecordView _recordView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private GameObject _congratulationBar;

        private readonly WaitForSeconds _delay = new (1.25f);

        private IStateMachine _stateMachine;
        private ICoroutineRunner _coroutineRunner;
        private ILevelSelector _levelSelector;
        private IAnswerCorrectnessService _answerCorrectnessService;
        private Question _currentQuestion;
        private Question[] _questions;
        private IStatsService _statsService;
        private int _rightAnswers;
        private IScoreService _scoreService;

        [Inject]
        public void Construct(IStateMachine stateMachine, 
            ICoroutineRunner coroutineRunner,
            ILevelSelector levelSelector,
            IAnswerCorrectnessService answerCorrectnessService,
            IScoreService scoreService,
            IStatsService statsService)
        {
            _scoreService = scoreService;
            _statsService = statsService;
            _levelSelector = levelSelector;
            _stateMachine = stateMachine;
            _answerCorrectnessService = answerCorrectnessService;
            _coroutineRunner = coroutineRunner;
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
            
            foreach (var button in _variantButtons)
                button.Subscribe(OnVariantButtonClicked);
            
            _recordView.TurnOn();
            _scoreView.TurnOn();
            _soundButton.TurnOn();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            
            foreach (var button in _variantButtons)
                button.Unsubscribe(OnVariantButtonClicked);
            
            _recordView.TurnOff();
            _scoreView.TurnOff();
            _soundButton.TurnOff();
        }

        public void Initialize(Question[] questions)
        {
            _congratulationBar.SetActive(false);
            
            _questions = questions;
            _levelText.text = "Level " + _levelSelector.SelectedLevel;
            _rightAnswers = 0;
            
            SetQuestion(_questions[0]);
        }

        private void OnVariantButtonClicked(string answer)
        {
            foreach (var variantButton in _variantButtons)
                variantButton.Disable();

            int questionIndex = _questions.FindIndex(_currentQuestion);

            if (_answerCorrectnessService
                .IsCorrectAnswer(answer, _currentQuestion.Answer.Name))
            {
                _statsService.AddCorrect();
                _rightAnswers++;
            }
            else
            {
                _statsService.AddIncorrect();
            }

            _coroutineRunner.StartCoroutine(ResetOverlay(questionIndex));
        }

        private void SetQuestion(Question question)
        {
            _currentQuestion = question;
            var sortedButtons = _variantButtons.SortRandomly();

            sortedButtons[0].Construct(question.Answer.Name, true);
            
            for (int i = 1; i < sortedButtons.Length; i++)
                sortedButtons[i].Construct(question.Other[i - 1].Name, false);
            
            _variantView.SetData(question.Answer);
        }

        private IEnumerator ResetOverlay(int questionIndex)
        {
            yield return _delay;
            
            foreach (var variantButton in _variantButtons)
                variantButton.Reset();

            if (_questions.FindIndex(_currentQuestion) == _questions.Length - 1)
            {
                _levelSelector.OpenNextLevelTo(_levelSelector.SelectedLevel);
                _scoreService.Reset();

                if(_rightAnswers == _questions.Length)
                    _congratulationBar.SetActive(true);
                else
                    _stateMachine.Enter<SaveDataState>();
                
                yield break;
            }
            
            SetQuestion(_questions[++questionIndex]);
        }

        private void OnBackButtonClick()
        {
            _stateMachine.Enter<SaveDataState>();
        }
    }
}