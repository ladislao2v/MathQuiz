namespace Code.Services.RecordService
{
    public interface IGameResult
    {
        int Level { get; }
        int PlayerScore { get; }
        int EnemyScore { get; }
    }
}