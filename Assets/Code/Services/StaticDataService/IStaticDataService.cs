using Code.Services.StaticDataService.Configs;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService
    {
        LevelConfig GetChampionship(int level);
    }
}