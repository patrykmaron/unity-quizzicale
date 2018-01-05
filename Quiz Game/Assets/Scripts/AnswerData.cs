[System.Serializable]
public class AnswerData
{
	public string answerText;
	public bool isCorrect;

    public string toString()
    {
        string all = answerText + "Result: " + isCorrect.ToString() + "\n";

        return all;
    }
}

