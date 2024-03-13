using System.Collections.Generic;
using Code.Services.SaveLoadDataService;

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
                .LoadByCustomKey<List<IGameResult>>(nameof(Results));
            
            if(results != null)
                _results.AddRange(results);
        }

        public void SaveData(ISaveLoadDataService saveLoadDataService)
        {
            saveLoadDataService
                .SaveByCustomKey(_results, nameof(Results));
        }
    }
}