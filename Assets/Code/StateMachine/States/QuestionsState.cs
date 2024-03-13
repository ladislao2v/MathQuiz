using Code.Services.LevelSelectorService;
using Code.Services.StaticDataService;
using Code.UI.Gameplay;

namespace Code.StateMachine.States
{
    public class QuestionsState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelSelector _levelSelector;
        private readonly GameplayOverlay _gameplayOverlay;

        public QuestionsState(IStateMachine stateMachine, IStaticDataService staticDataService, ILevelSelector levelSelector, GameplayOverlay gameplayOverlay)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _levelSelector = levelSelector;
            _gameplayOverlay = gameplayOverlay;
        }

        public void Enter()
        {
            _gameplayOverlay.Show();

            SetQuestions();
        }

        private void SetQuestions()
        {
            var level = _staticDataService
                .GetChampionship(_levelSelector.SelectedLevel);

            var logo = level.Logo;
            var questions = level.Questions;

            _gameplayOverlay.Initialize(logo, questions);
        }

        public void Exit()
        {
            _gameplayOverlay.Hide();
        }
    }
}