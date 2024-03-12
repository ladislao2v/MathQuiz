using Code.Services.SaveLoadDataService;

namespace Code.Services.StatsService
{
    public class StatsService : IStatsService
    {
        public int CorrectAnswers { get; private set; }
        public int IncorrectAnswers { get; private set; }
        
        public void AddCorrect() => CorrectAnswers++;

        public void AddIncorrect() => IncorrectAnswers++;
        public void LoadData(ISaveLoadDataService saveLoadDataService)
        {
            CorrectAnswers = saveLoadDataService
                .LoadByCustomKey<int?>(nameof(CorrectAnswers))
                .GetValueOrDefault();
            
            IncorrectAnswers = saveLoadDataService
                .LoadByCustomKey<int?>(nameof(IncorrectAnswers))
                .GetValueOrDefault();
        }

        public void SaveData(ISaveLoadDataService saveLoadDataService)
        {
            saveLoadDataService.SaveByCustomKey(CorrectAnswers, nameof(CorrectAnswers));
            saveLoadDataService.SaveByCustomKey(IncorrectAnswers, nameof(IncorrectAnswers));
        }
    }
}