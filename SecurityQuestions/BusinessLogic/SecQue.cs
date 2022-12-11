using SecurityQuestions.Model;
using System.Diagnostics;

namespace SecurityQuestions.BusinessLogic;

public class SecQue
{
    private static readonly string[] Questions = new[]
{
        "In what city were you born?",
        "What is the name of your favorite pet?",
        "What is your mother's maiden name?",
        "What high school did you attend?",
        "What was the mascot of your high school?",
        "What was the make of your first car?",
        "What was your favorite toy as a child?",
        "Where did you meet your spouse?",
        "What is your favorite meal?",
        "Who is your favorite actor / actress?",
        "What is your favorite album"
    };

    //
    // My readline with exception handling
    //
    private String MyReadLine() {
        String response = "";

        try {
            response = Console.ReadLine()!;
        } catch (Exception ex) {
            Console.WriteLine("Something went wrong reading from console, " + ex.Message);
        }
        return response;
    }

    //
    // My readline with exception handling
    //
    private int MyReadKey() {
        int resp = 0;

        try {
            resp = Console.ReadKey().KeyChar;
        } catch (Exception ex) {
            Console.WriteLine("Something went wrong reading keystroke from console, " + ex.Message);
        }
        return resp;
    }

    //
    // Prompt the user on the command line for name
    //
    private String PromptName() {
        String Name = "";

        Console.WriteLine();
        Console.WriteLine(Resources.Program.ASK_NAME);

        // user should enter at least 2 character for name
        while (true) {
            Name = MyReadLine();

            if (Name.Length < 2) {
                Console.WriteLine(Resources.Program.MIN_2_CHARACTERS);
            } else {
                Debug.WriteLine("User entered name: " + Name);
                break;
            }
        };

        return Name.ToString();
    }

    //
    // Prompt user to answer security questions
    // return: bool
    //
    private bool PromptStoreQuestions() {
        bool bSkipStoreFlow = false;

        Console.WriteLine(Resources.Program.STORE_ANSWERS);

        do {
            Console.WriteLine(Resources.Program.ENTER_Y_OR_N);
            int input = MyReadKey();
            Console.WriteLine();
            Console.WriteLine();

            // 121 is ascii value of lower case 'y' and 110 is ascii value of lower case 'n'
            if (input == 121) {
                bSkipStoreFlow = true;
                Debug.WriteLine("You entered Yes");
                break;
            } else if (input == 110) {
                Debug.WriteLine("You entered No");
                break;
            }
        } while (true);

        return bSkipStoreFlow;
    }

    //
    // Prompt user to answer security questions
    // return: bool
    //
    private bool PromptAnswerQuestions() {
        bool bSkipStoreFlow = false;

        Console.WriteLine(Resources.Program.ANSWER_QUESTIONS);

        do {
            Console.WriteLine(Resources.Program.ENTER_Y_OR_N);
            int input = MyReadKey();
            Console.WriteLine();
            Console.WriteLine();

            // 121 is ascii value of lower case 'y' and 110 is ascii value of lower case 'n'
            if (input == 121) {
                bSkipStoreFlow = true;
                Debug.WriteLine("You entered Yes");
                break;
            } else if (input == 110) {
                Debug.WriteLine("You entered No");
                break;
            }
        } while (true);

        return bSkipStoreFlow;
    }

    //
    // User agreed to store questions. Loop thru questions until the user answered 3 questions or the reach the end of the list. Customer can skip questions with enter key.
    //
    private void StoreQuestions(String name, Users users) {
        User user = new User();
        user.Name = name;
        int count = 0;

        for (int i = 0; i < Questions.Length;) {
            String response = "";

            Console.WriteLine();
            Console.WriteLine(Resources.Program.ENTER_ANSWER);
            Console.WriteLine(Questions[i]);
            response = MyReadLine();

            if (response.Length == 0) {
                Console.WriteLine(Resources.Program.SKIP_QUESTION);
                i++;
            } else if (response.Length < 2) {
                Console.WriteLine();
                Console.WriteLine(Resources.Program.MIN_2_CHARACTERS);
            } else {
                var answer = new Answer(i, response);
                user.Answers.Add(answer);
                Debug.WriteLine("The user entered Answer: " + answer);
                count++;
                if (count == 3) {
                    users.AddOrUpdate(user);
                    users.Save();
                    i = Questions.Length;
                } else {
                    i++;
                }
            }
        }

        if (count < 3) {
            Console.WriteLine();
            Console.WriteLine(Resources.Program.OUT_OF_QUESTIONS);
        }
    }

    //
    // User agreed to answer questions. Loop thru questions until the user answered 3 questions or the reach the end of the list. Customer can skip questions with enter key.
    //
    private void AnswerQuestions(User existingUser) {
        String response = "";
        bool answered = false;

        foreach (Answer anAnswer in existingUser.Answers) {
            Console.WriteLine();
            Console.WriteLine(Resources.Program.ENTER_RESPONSE);
            Console.WriteLine(Questions[anAnswer.Id]);
            response = MyReadLine();

            if (response.Equals(anAnswer.Response)) {
                Console.WriteLine();
                Console.WriteLine(Resources.Program.CORRECT_RESPONSE);
                answered = true;
                break;
            } else {
                Console.WriteLine();
                Console.WriteLine(Resources.Program.INCORRECT_RESPONSE);
            }
        }

        if (!answered) {
            Console.WriteLine();
            Console.WriteLine(Resources.Program.OUT_OF_QUESTIONS);
        }
    }

    private void StoreFlow(String name, Users users) {
        if (!PromptStoreQuestions()) {
            Debug.WriteLine("User doesn't want to store questions");
        } else {
            Debug.WriteLine("Willing to store questions, start prompting for reponses");
            StoreQuestions(name, users);
        }
    }

    private bool AnswerFlow(User existingUser) {
        bool answer = PromptAnswerQuestions();

        if (!answer) {
            Debug.WriteLine("User doesn't want to answer questions, switch to store flow.");
        } else {
            Debug.WriteLine("Willing to answer questions, start asking them");
            AnswerQuestions(existingUser);
        }

        return answer;
    }

    public void Run() {
        Users users = new Users();

        // creating an infinite loop.
        do {
            String name = "";

            // Prompt for the users name
            name = PromptName();
            if (name.ToLower().Equals("quit")) {
                Console.WriteLine();
                Console.WriteLine(Resources.Program.ENTER_Y_OR_N);
                break;
            } else {
                Console.WriteLine();
                Console.WriteLine(Resources.Program.GREETING + name);
                Console.WriteLine();
            }

            if (users.Match(name)) {
                Debug.WriteLine("Found a match");
                User existingUser = users.getUser(name);

                if (!AnswerFlow(existingUser)) {
                    StoreFlow(name, users);
                }
            } else {
                Debug.WriteLine("Match NOT found");
                StoreFlow(name, users);
            }

        } while (true);
    }
}
