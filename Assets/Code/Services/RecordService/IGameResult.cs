namespace Code.Services.RecordService
{
    public interface IGameResult
    {
        int PlayerScore { get; }
        int EnemyScore { get; }
    }
}