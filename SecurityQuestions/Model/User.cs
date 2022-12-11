using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.Model;

public class User
{
    public String Name { get; set; }
    public List<Answer> Answers { get; set; }

    public User() {
        Name = "";
        Answers = new List<Answer>();
    }

    public override string ToString() {
        String returnString = "Name: " + Name + ", Answers: ";

        foreach (Answer anAnswer in Answers) {
            returnString = returnString + "answer: " + anAnswer;
        }

        return returnString;   
    }
}
