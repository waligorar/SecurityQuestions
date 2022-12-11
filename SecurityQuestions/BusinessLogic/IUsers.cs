using SecurityQuestions.Model;

namespace SecurityQuestions.BusinessLogic
{
    internal interface IUsers
    {
        void AddOrUpdate(User user);
        User getUser(String name);
        bool Match(String name);
        bool Equals(object? obj);
        int GetHashCode();
        void Save();
        string? ToString();
    }
}