using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.Networking;

public class CreateQuestionsController : MonoBehaviour {

    public InputField questionInput;
    public InputField answerA;
    public InputField answerB;
    public InputField answerC;
    public InputField answerD;

    public Toggle togA;
    public Toggle togB;
    public Toggle togC;
    public Toggle togD;

    public Text questionNumberDisplay;

   // private QuestionData[] questions = new QuestionData[20];
    private List<QuestionData> questionList = new List<QuestionData>();

    private int currentQuestionViewing;

    private string gameDataProjectFilePath = "/StreamingAssets/TimdataTest.json";


    private bool isEditing;

    private DataController dataController;

    // Use this for initialization
    void Start() {

        //Test Questions

        dataController = dataController = FindObjectOfType<DataController>();

        isEditing = dataController.getisEditig();

        if (isEditing)
        {

            RoundData data = dataController.GetCurrentRoundData();

            QuestionData[] questionsData = data.questions;

            questionList = new List<QuestionData>(questionsData);
            loadQuestion(0);

        }
        else
        {

            questionInput.text = "What is my name?";

            answerA.text = "Tim";
            answerB.text = "John";
            answerC.text = "Barry";
            answerD.text = "Ben";

            togA.isOn = true;
            togB.isOn = false;
            togC.isOn = false;
            togD.isOn = false;

        }
        currentQuestionViewing = 0;
    }

    // Update is called once per frame
    void Update() {

        questionNumberDisplay.text = "Question Number: " + currentQuestionViewing;


    }

    public void addQuestion()
    {
        
        QuestionData question = collectData();

        if (currentQuestionViewing >= questionList.Count)
        {
            questionList.Add(question);
            print("Added Question to List");
            nextQuestion();
        }
        else
        {
            questionList[currentQuestionViewing] = question;
            print("Updated Question " + currentQuestionViewing);
        }
        
         
        
    }

    public void saveQuestions()
    {
        string gameDataProjectFilePath = "/StreamingAssets/data.json";
        GameData gameData = new GameData();

        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }

    private void clearTextBoxes()
    {
        questionInput.text = "";
        
        answerA.text = "";
        answerB.text = "";
        answerC.text = "";
        answerD.text = "";

        togA.isOn = false;
        togB.isOn = false;
        togC.isOn = false;
        togD.isOn = false;
    }

    private void loadQuestion(int refNumber)
    {

        if (refNumber < questionList.Count)
        {
            
            QuestionData questMe = questionList[refNumber];
            AnswerData[] ansData = questMe.answers;

            questionInput.text = questMe.questionText;

            answerA.text = ansData[0].answerText;
            answerB.text = ansData[1].answerText;
            answerC.text = ansData[2].answerText;
            answerD.text = ansData[3].answerText;

            togA.isOn = ansData[0].isCorrect;
            togB.isOn = ansData[1].isCorrect;
            togC.isOn = ansData[2].isCorrect;
            togD.isOn = ansData[3].isCorrect;
            print("Loaded question " + refNumber);

        }
        else
        {

            clearTextBoxes();
            print("Cleared Textboxes");

        }

    }

    public void nextQuestion()
    {

        

        if(currentQuestionViewing >= questionList.Count)
        {
            if (currentQuestionViewing > questionList.Count )
            {
                print("NO MORE QUESTIONS TO VIEW");
                
            }
            else
            {
                print("NEW QUESTION");
                clearTextBoxes();
               
            }
        }
        else
        {
            print("Hit Load Question");
            currentQuestionViewing++;
            loadQuestion(currentQuestionViewing);
            questionNumberDisplay.text = "Question Number: " + currentQuestionViewing;
        }

    }

    public void previousQuestion()
    {
        

        if (currentQuestionViewing < 1)
        {
           
                print("NO MORE QUESTIONS TO VIEW");
                
           
        }
        else
        {
            currentQuestionViewing--;
            print("Hit Load Question");
            loadQuestion(currentQuestionViewing);
            questionNumberDisplay.text = "Question Number: " + currentQuestionViewing;
        }
    }

  

    public void removeQuestion()
    {

        questionList.RemoveAt(currentQuestionViewing);
        clearTextBoxes();

    }

    private QuestionData collectData()
    {

        AnswerData[] answersFinal = new AnswerData[4];
        QuestionData question = new QuestionData();
        question.questionText = questionInput.text;

        string[] formAnswers = new string[4];


        formAnswers[0] = answerA.text;
        formAnswers[1] = answerB.text;
        formAnswers[2] = answerC.text;
        formAnswers[3] = answerD.text;

        bool[] correctAnswers = new bool[4];

        correctAnswers[0] = togA.isOn;
        correctAnswers[1] = togB.isOn;
        correctAnswers[2] = togC.isOn;
        correctAnswers[3] = togD.isOn;

        int answerCount = 0;



        for (int i = 0; i < formAnswers.Length; i++)
        {

            AnswerData ansCurrent = new AnswerData();
            ansCurrent.answerText = formAnswers[i];
            ansCurrent.isCorrect = correctAnswers[i];
            answersFinal[answerCount] = ansCurrent;
            answerCount++;

        }

        question.answers = answersFinal;

        return question;

    }

    public void saveQuiz()
    {
        GameData gameData = new GameData();
        RoundData roundData;

        if (isEditing)
        {
            roundData = dataController.GetCurrentRoundData();
        }
        else
        {
            roundData = dataController.addRoundData;

        }

        roundData.questions = questionList.ToArray();

        gameDataProjectFilePath = "/StreamingAssets/" + roundData.name + ".json";

        RoundData[] roundDataArray = new RoundData[1];
        roundDataArray[0] = roundData;
        gameData.allRoundData = roundDataArray;


        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
        StartCoroutine(UploadQuizToDatabase(filePath));
    }

    IEnumerator UploadQuizToDatabase(string file)
    {
        string filePath = file;
        string dataAsJSON = File.ReadAllText(filePath);
        var utf8 = Encoding.UTF8;
        byte[] utfBytes = utf8.GetBytes(dataAsJSON);
        string dataToSend = utf8.GetString(utfBytes, 0, utfBytes.Length);


        string dataToJson = JsonUtility.ToJson(dataAsJSON);
        Debug.Log(dataAsJSON);
        string url = "http://ehc45e1ry0.execute-api.eu-west-2.amazonaws.com/Development/create-question";

        UnityWebRequest request = UnityWebRequest.Post(url, dataAsJSON);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.Send();
        Debug.Log(request.responseCode);
    }

}
