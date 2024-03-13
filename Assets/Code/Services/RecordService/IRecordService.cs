using System.Collections.Generic;
using Code.Services.SaveLoadDataService;

namespace Code.Services.RecordService
{
    public interface IRecordService : ILoadable, ISavable
    {
        IReadOnlyList<IGameResult> Results { get; }
        void Save(IGameResult result);
    }
}