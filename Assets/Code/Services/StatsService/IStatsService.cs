using Code.Services.SaveLoadDataService;

namespace Code.Services.StatsService
{
    public interface IStatsService : ILoadable, ISavable
    {
        int CorrectAnswers { get; }
        int IncorrectAnswers { get; }

        void AddCorrect();
        void AddIncorrect();
    }
}