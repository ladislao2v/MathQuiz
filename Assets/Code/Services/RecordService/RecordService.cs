using System.Collections.Generic;
using System.Linq;
using Code.Services.SaveLoadDataService;
using Code.UI;

namespace Code.Services.RecordService
{
    public class RecordService : IRecordService
    {
        private readonly Dictionary<int, IGameResult> _results = new()
        {
            [1] = null,
            [2] = null,
            [3] = null
        };
        
        public IReadOnlyList<IGameResult> Results => _results.Values.ToList();

        public void Save(IGameResult result)
        {
            _results[result.Level] = result;
        }

        public void LoadData(ISaveLoadDataService saveLoadDataService)
        { 
            var results = saveLoadDataService
                .LoadByCustomKey<List<GameResult>>(nameof(Results));

            if (results == null)
                return;

            _results.Clear();
            
            foreach (var result in results)
            {
                _results[result.Level] = result;
            }
        }

        public void SaveData(ISaveLoadDataService saveLoadDataService)
        {
            List<GameResult> results = new();
            
            foreach (var pair in _results)
            {
                if(pair.Value != null)
                    results.Add((GameResult)pair.Value);
            }
            
            saveLoadDataService
                .SaveByCustomKey(results, nameof(Results));
        }
    }
}