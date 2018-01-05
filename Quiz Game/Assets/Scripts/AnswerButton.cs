using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerButton : MonoBehaviour 
{
	public Text answerText;

	private GameController gameController;
	private AnswerData answerData;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	public void SetUp(AnswerData data)
	{
		answerData = data;
		answerText.text = answerData.answerText;
	}

    public void SetUp(string answer, Question data)
    {
        AnswerData temp = new AnswerData();
        temp.answerText = answer;
        if(answer == data.correctAnswer)
        {
            temp.isCorrect = true;
        }
        else
        {
            temp.isCorrect = false;
        }
        answerData = temp;
        answerText.text = answer;
    }

	public void HandleClick()
	{
		gameController.AnswerButtonClicked(answerData.isCorrect);
	}
}
