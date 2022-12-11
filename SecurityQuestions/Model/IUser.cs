namespace SecurityQuestions.Model
{
    public interface IUser
    {
        List<Answer> Answers { get; set; }
        string Name { get; set; }

        string ToString();
    }
}