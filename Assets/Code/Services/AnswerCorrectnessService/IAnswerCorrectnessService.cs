namespace Code.Services.AnswerCorrectnessService
{
    public interface IAnswerCorrectnessService
    {
        bool IsCorrectAnswer(string answer, string correctAnswer);
    }
}