using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameController : MonoBehaviour {

    public Text quizName;
    public Text timelimtInSeconds;
    public Text pointsPerQuestion;

    private DataController dataController;

    void Start()
    {
        dataController = FindObjectOfType<DataController>();



    }

        public void addGame()
    {


        RoundData round = new RoundData();

        round.name = quizName.text;

        round.timeLimitInSeconds = int.Parse(timelimtInSeconds.text);
        round.pointsAddedForCorrectAnswer = int.Parse(pointsPerQuestion.text);

        dataController.addRoundData = round;

        print(round.name);

        print(round.ToString());

        GlobalObject.addRoundData = round;
            

        UnityEngine.SceneManagement.SceneManager.LoadScene("CreateQuestions");
    }

    public void backToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScreen");

    }

}
