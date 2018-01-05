[System.Serializable]
public class QuestionData
{
	public string questionText;
	public AnswerData[] answers;

    public string toString()
    {
        string answersWritten = "";

        for (int i = 0; i < answers.Length; i++)
        {
            AnswerData ans = answers[i];

            answersWritten = answersWritten + ans.toString() + "\n";
        }

        return "Question: " + questionText + "\n" + answersWritten;
    }
}