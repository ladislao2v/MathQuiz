using System.Collections.Generic;
using System.Linq;
using Code.Services.SaveLoadDataService;
using Code.UI;

namespace Code.Services.RecordService
{
    public class RecordService : IRecordService
    {
        private readonly List<IGameResult> _results = new();
        
        public IReadOnlyList<IGameResult> Results => _results;

        public void Save(IGameResult result)
        {
            _results.Add(result);
        }

        public void LoadData(ISaveLoadDataService saveLoadDataService)
        { 
            var results = saveLoadDataService
                .LoadByCustomKey<List<GameResult>>(nameof(Results));
            
            if(results != null)
                _results.AddRange(results);
        }

        public void SaveData(ISaveLoadDataService saveLoadDataService)
        {
            List<GameResult> results = new();
            
            foreach (var result in _results)
            {
                results.Add((GameResult)result);
            }
            
            saveLoadDataService
                .SaveByCustomKey(results, nameof(Results));
        }
    }
}