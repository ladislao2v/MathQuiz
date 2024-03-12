using Code.Services.LevelSelectorService;
using Code.Services.ScoreService;

namespace Code.Services.AnswerCorrectnessService
{
    public class AnswerCorrectnessService : IAnswerCorrectnessService
    {
        private readonly ILevelSelector _levelSelector;
        private readonly IScoreService _scoreService;

        public AnswerCorrectnessService(ILevelSelector levelSelector, IScoreService scoreService)
        {
            _levelSelector = levelSelector;
            _scoreService = scoreService;
        }
        public bool IsCorrectAnswer(string answer, string correctAnswer)
        {
            if (answer == correctAnswer)
            {
                _scoreService.Add(_levelSelector.SelectedLevel);
                return true;
            }

            return false;
        }
    }
}