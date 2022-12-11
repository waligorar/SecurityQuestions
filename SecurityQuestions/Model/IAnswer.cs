namespace SecurityQuestions.Model
{
    public interface IAnswer
    {
        int Id { get; set; }
        string Response { get; set; }

        bool Equals(Answer? other);
        string ToString();
    }
}