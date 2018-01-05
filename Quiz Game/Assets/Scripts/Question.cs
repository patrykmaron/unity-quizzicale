using System.Collections;
using System.Collections.Generic;

public class Question{

    public int questionID;
    public int quizID;
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public List<string> allAnswers = new List<string>();
    public string correctAnswer;
    public int totalAnswers;

    public Question()
    {

    }

}
