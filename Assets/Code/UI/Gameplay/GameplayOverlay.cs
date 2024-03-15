using System.Collections;
using Code.Extensions;
using Code.Services.AnswerCorrectnessService;
using Code.Services.CoroutineRunner;
using Code.Services.LevelSelectorService;
using Code.Services.PauseService;
using Code.Services.ScoreService;
using Code.Services.StaticDataService.Configs;
using Code.Services.StatsService;
using Code.Services.TimerService;
using Code.StateMachine;
using Code.StateMachine.States;
using Code.UI.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Gameplay
{
    public class GameplayOverlay : Overlay
    {
        [SerializeField] private Image _levelLogo;
        [SerializeField] private Button _backButton;
        [SerializeField] private VariantView _variantView;
        [SerializeField] private VariantButton[] _variantButtons;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private HalfEndView _halfEndView;
        [SerializeField] private ResultView _resultView;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private PauseView _pauseView;

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
        private ITimer _timer;
        private int _half = 1;
        private IPauseService _pauseService;


        [Inject]
        public void Construct(IStateMachine stateMachine, 
            ICoroutineRunner coroutineRunner,
            ILevelSelector levelSelector,
            IAnswerCorrectnessService answerCorrectnessService,
            IScoreService scoreService,
            IStatsService statsService,
            ITimer timer,
            IPauseService pauseService)
        {
            _pauseService = pauseService;
            _timer = timer;
            _scoreService = scoreService;
            _statsService = statsService;
            _levelSelector = levelSelector;
            _stateMachine = stateMachine;
            _answerCorrectnessService = answerCorrectnessService;
            _coroutineRunner = coroutineRunner;
        }

        private void OnEnable()
        {
            _pauseView.TurnOn();
            _resultView.TurnOn();
            _halfEndView.TurnOn();
            _backButton.onClick.AddListener(OnBackButtonClick);
            _halfEndView.NextHalf.onClick.AddListener(OnNextHalf);
            _pauseView.ContinueButton.onClick.AddListener(OnContinue);
            _pauseView.RestartButton.onClick.AddListener(OnRestart);
            _pauseView.ExitButton.onClick.AddListener(OnExit);
            _pauseButton.Subscribe(OnPause);
            
            foreach (var button in _variantButtons)
                button.Subscribe(OnVariantButtonClicked);
            
            _scoreView.TurnOn();
            _timer.Ticked += _timerView.Render;
            _timer.TimeOut += OnTimeOut;
        }

        private void OnDisable()
        {
            _pauseView.TurnOff();
            _resultView.TurnOff();
            _halfEndView.TurnOff();
            _backButton.onClick.RemoveListener(OnBackButtonClick);
            _halfEndView.NextHalf.onClick.RemoveListener(OnNextHalf);
            _pauseView.ContinueButton.onClick.RemoveListener(OnContinue);
            _pauseView.RestartButton.onClick.RemoveListener(OnRestart);
            _pauseView.ExitButton.onClick.RemoveListener(OnExit);
            _pauseButton.Unsubscribe(OnPause);
            
            foreach (var button in _variantButtons)
                button.Unsubscribe(OnVariantButtonClicked);
            
            _scoreView.TurnOff();
        }

        private void OnPause()
        {
            _pauseService.Pause();
            _pauseView.Show();
            _scoreService.Update();
        }

        public void Initialize(Sprite logo, Question[] questions)
        {
            _questions = questions;
            _levelLogo.sprite = logo;
            _rightAnswers = 0;
            
            SetQuestion(_questions[0]);
            _timerView.Construct(_half);
            _timer.Start();
        }

        private void OnVariantButtonClicked(string answer)
        {
            foreach (var variantButton in _variantButtons)
                variantButton.Disable();

            int questionIndex = _questions.FindIndex(_currentQuestion);

            if (_answerCorrectnessService
                .IsCorrectAnswer(answer, _currentQuestion.Answer))
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

            sortedButtons[0].Construct(question.Answer, true);
            
            for (int i = 1; i < sortedButtons.Length; i++)
                sortedButtons[i].Construct(question.Other[i - 1], false);
            
            _variantView.SetData(question.Task);
        }

        private IEnumerator ResetOverlay(int questionIndex)
        {
            yield return _delay;
            
            foreach (var variantButton in _variantButtons)
                variantButton.Reset();

            if (_questions.FindIndex(_currentQuestion) == _questions.Length - 1)
            {
                _levelSelector.OpenNextLevelTo(_levelSelector.SelectedLevel);
                //_scoreService.Reset();

                if(_rightAnswers == _questions.Length)
                    Finish();
                else
                    _stateMachine.Enter<SaveDataState>();
                
                yield break;
            } 
            
            SetQuestion(_questions[++questionIndex]);
        }

        private void OnContinue()
        {
            _pauseService.Resume();
            _pauseView.Hide();
        }
        
        private void OnRestart()
        {
            _stateMachine.Enter<QuestionsState>();
        }

        private void OnExit()
        {
            _stateMachine.Enter<SaveDataState>();
        }

        private void OnTimeOut()
        {
            _half++;

            if (_half == 3)
            {
                Finish();
            }
            
            _timerView.Construct(_half);
            _halfEndView.Show();
            _scoreService.Update();
        }

        private void Finish()
        {
            var isWin = _scoreService.IsPlayerWin();
            _resultView.Construct(isWin);
            _resultView.Show();
            _scoreService.Update();
            _levelSelector.OpenNextLevelTo(_levelSelector.SelectedLevel);
        }

        private void OnNextHalf()
        {
            _halfEndView.Hide();
            _timer.Start();
        }

        private void OnBackButtonClick()
        {
            _stateMachine.Enter<SaveDataState>();
        }
    }
}