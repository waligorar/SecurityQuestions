using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.Model;

public class Answer : IEquatable<Answer>, IAnswer
{
    public int Id { get; set; }
    public string Response { get; set; }

    public Answer(int id, string response) {
        Id = id;
        Response = response;
    }

    public override string ToString() {
        return "Id: " + Id + ", Response: " + Response;
    }

    public bool Equals(Answer? other) {
        throw new NotImplementedException();
    }
}
