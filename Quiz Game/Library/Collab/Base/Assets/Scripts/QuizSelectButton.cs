using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSelectButton : MonoBehaviour {

    public Text displayText;

    RoundData data;

    private QuizSelectionController quizSelectionController;


    void Start()
    {
        quizSelectionController = FindObjectOfType<QuizSelectionController>();
    }

    public void SetUp(RoundData data)
    {
        this.data = data;
        displayText.text = data.name;
    }

    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }
}
