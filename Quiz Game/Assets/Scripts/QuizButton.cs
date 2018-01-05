using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizButton : MonoBehaviour {

    public Text quizText;

    private QuizSelectionController quizController;
    private RoundData roundData;

	// Use this for initialization
	void Start () {

        quizController = FindObjectOfType<QuizSelectionController>();
    }

    public void SetUp(RoundData data)
    {
        roundData = data;
        quizText.text = roundData.name;
    }
	
public void HandleClick()
    {
        quizController.QuestionButtonClicked(roundData);
    }
}
