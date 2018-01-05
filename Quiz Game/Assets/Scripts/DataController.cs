using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;

public class DataController : MonoBehaviour 
{
	private RoundData[] allRoundData;

    public  RoundData addRoundData;

    private PlayerProgress playerProgress;
    private string gameDataFileName = "data.json";
    private FileInfo[] info;

    private RoundData currentRound;

    private bool isEditing;

    private int selectedRound;

    private List<RoundData> roundsFromDatabase = new List<RoundData>();

    void Start ()  
	{
		DontDestroyOnLoad (gameObject);
        LoadGameData();
        LoadPlayerProgress();
        LoadQuizLists();
        StartCoroutine(GetRoundsFromDatabase());
        allRoundData = getRoundsData();



        SceneManager.LoadScene ("MenuScreen");

	}
	
	public RoundData GetCurrentRoundData()
	{
		return allRoundData [0];
	}

    public void isEditingSet(bool b)
    {
        isEditing = b;
    }

    public bool getisEditig()
    {
        return isEditing;
    }

    //Get the question data when the game starts
    public RoundData GetSelectedRound()
    {
        
        return currentRound;
    }

    public void setCurrentRound(RoundData data)
    {
        
        currentRound = data;
        
    }

    public void GetQuestions(RoundData round)
    {
        currentRound = round;
        StartCoroutine(GetQuestionsFromDatabase());
    }

    public void SunbmitNewPlayerScore(int newScore)
    {
        if(newScore > playerProgress.highestScore)
        {

            playerProgress.highestScore = newScore;
            SavePlayerProgress();

        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");

        }

    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    private void LoadGameData()
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJSON);

            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }

    }

    private void LoadQuizLists()
    {
        

        //get multiple JSONs
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        List<string> filePaths = new List<string>();

     
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        info = dir.GetFiles("*.json*");
        foreach (FileInfo f in info)
        {

            print(f.FullName);

        }

            //get all and return round data

            if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJSON);

            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }


    }

    public string[] getRoundList()
    {
        List<string> fileNames = new List<string>();

        /*foreach (FileInfo f in info)
        {
            if (f.Extension == ".json")
            {
                print(f.FullName);
                string dataAsJSON = File.ReadAllText(f.FullName);
                GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJSON);
                RoundData loadedRoundData = loadedData.allRoundData[0];

                print(loadedRoundData.name);
                fileNames.Add(loadedRoundData.name);
            }
            else
            {
                Debug.LogError("Cannot load game data from "+ f.FullName);
            }
        }*/

        for(int i = 0; i < roundsFromDatabase.Count; i++)
        {
            fileNames.Add(roundsFromDatabase[i].name);
        }

        return fileNames.ToArray();

    }

    WWW temp;

    IEnumerator GetRoundsFromDatabase()
    {
        string url = "http://ehc45e1ry0.execute-api.eu-west-2.amazonaws.com/Development/get-quizzes";


        WWW getData = new WWW(url);
        temp = getData;
        yield return getData;
        JSONNode newTest = JSONArray.Parse(getData.text);
        
        for(int i = 0; i < newTest.Count; i++)
        {
            RoundData tempData = new RoundData();
            tempData.name = newTest[i]["QuizName"];
            tempData.pointsAddedForCorrectAnswer = newTest[i]["Points"];
            tempData.quizID = newTest[i]["QuizID"];
            tempData.timeLimitInSeconds = newTest[i]["TimeLimit"];
            roundsFromDatabase.Add(tempData);
        }
        
    }

    IEnumerator GetQuestionsFromDatabase()
    {
        int quizID = currentRound.quizID;
        string url = "https://ehc45e1ry0.execute-api.eu-west-2.amazonaws.com/Development/get-quiz-questions?quizID=" + quizID;
        WWW getData = new WWW(url);
        yield return  getData;
       // yield return getData;
        JSONNode newTest = JSONArray.Parse(getData.text);
        List<Question> tempQuestions = new List<Question>();

        for (int i = 0; i < newTest.Count; i++)
        {
            int nullAnswerCounter = 0;
            Question tempQuestion = new Question();
            tempQuestion.quizID = quizID;
            tempQuestion.questionID = newTest[i]["QuestionID"];
            tempQuestion.question = newTest[i]["Question"];
            tempQuestion.answer1 = newTest[i]["Answer1"];
            tempQuestion.allAnswers.Add(tempQuestion.answer1);
            tempQuestion.answer2 = newTest[i]["Answer2"];
            tempQuestion.allAnswers.Add(tempQuestion.answer2);
            if (newTest[i]["Answer3"] == null || newTest[i]["Answer3"] == "null")
            {
                nullAnswerCounter++;
            }
            else
            {
                tempQuestion.answer3 = newTest[i]["Answer3"];
                tempQuestion.allAnswers.Add(tempQuestion.answer3);
            }
            
            if (newTest[i]["Answer4"] == null || newTest[i]["Answer4"] == "null")
            {
                nullAnswerCounter++;
            }
            else
            {
                tempQuestion.answer4 = newTest[i]["Answer4"];
                tempQuestion.allAnswers.Add(tempQuestion.answer4);
            }
            tempQuestion.correctAnswer = newTest[i]["CorrectAnswer"];
            tempQuestion.totalAnswers = 4 - nullAnswerCounter;
            tempQuestions.Add(tempQuestion);
            Debug.Log("QUESTION: " + newTest[i]["Question"]);
        }

        currentRound.questionArray = tempQuestions.ToArray();
        
    }

        public RoundData[] getRoundsData()
    {
        /*List<RoundData> fileNames = new List<RoundData>();

        foreach (FileInfo f in info)
        {
            if (f.Extension == ".json")
            {
                print(f.FullName);
                string dataAsJSON = File.ReadAllText(f.FullName);
                GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJSON);
                RoundData loadedRoundData = loadedData.allRoundData[0];

                fileNames.Add(loadedRoundData);
            }
            else
            {
                Debug.LogError("Cannot load game data from " + f.FullName);
            }
        }*/



        // return fileNames.ToArray();
        return roundsFromDatabase.ToArray();

    }
}