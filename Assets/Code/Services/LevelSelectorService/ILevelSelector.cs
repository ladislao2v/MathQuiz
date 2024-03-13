using Code.Services.SaveLoadDataService;
using Code.Services.StaticDataService;
using Code.Services.StaticDataService.Configs;
using Unity.VisualScripting;

namespace Code.Services.LevelSelectorService
{
    public interface ILevelSelector : ILoadable, ISavable
    {
        int SelectedLevel { get; }
        int LevelCount { get; }
        void Select(int levelIndex);
        void OpenNextLevelTo(int levelIndex);
        bool IsOpen (int levelIndex);
    }
}