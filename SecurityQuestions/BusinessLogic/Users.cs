using SecurityQuestions.Model;
using System.Diagnostics;
using System.Text.Json;

namespace SecurityQuestions.BusinessLogic;

class Users : IUsers
{
    String fileName = "UsersList.json";
    List<User> users = new List<User>();

    public Users() {
        // Deserialize on load
        JsonSerializerOptions options = new() {
            PropertyNameCaseInsensitive = true
        };

        try {
            users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(fileName), options)!;
        } catch (FileNotFoundException fnfe) {
            // Ignore this error, file won't exist on first execution.
            Debug.WriteLine("Ignoring file not found exception, " + fnfe.Message);
        } catch (Exception ex) {
            Console.WriteLine("Something went wrong deserializing users list.");
            Console.WriteLine(ex.Message);
        }
    }

    public void AddOrUpdate(User user) {
        if (Match(user.Name)) {
            User existingUser = getUser(user.Name);
            existingUser.Answers = user.Answers;
        } else {
            users.Add(user);
        }
    }

    public User getUser(String name) {
        User user = new User();

        foreach (User aUser in users) {
            if (aUser.Name.Equals(name)) {
                user = aUser;
                break;
            }
        }
        return user;
    }

    public bool Match(String name) {
        bool found = false;

        foreach (User aUser in users) { 
            if (aUser.Name.Equals(name)) {
                found = true;
                break;
            }
        }
        return found;
    }

    public async void Save() {
        try {
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, users);
            await createStream.DisposeAsync();
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        Debug.WriteLine(File.ReadAllText(fileName));
    }

    public override bool Equals(object? obj) {
        return base.Equals(obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(users);
    }

    public override string? ToString() {
        String returnString = "";

        foreach (User aUser in users) {
            returnString = returnString + aUser.ToString();
        }

        return returnString;
    }
}
