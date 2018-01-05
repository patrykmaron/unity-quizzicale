using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizSelectionController : MonoBehaviour {

    //public Text textBox;

    public SimpleObjectPool quizbutonObjectPool;
    public Transform quizButtonParent;



    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private DataController dataController;
    private RoundData[] roundDataPool;

    private string[] quizList;
    private int[] quizListIDs;

    private RoundData[] roundList;

   

    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        quizList = dataController.getRoundList();

        roundList = dataController.getRoundsData();
        ShowQuestion();

    }

    public void QuestionButtonClicked(RoundData round)
    {
        dataController.GetQuestions(round);
        StartCoroutine(TestLoader(round));
    }

    public IEnumerator TestLoader(RoundData data)
    {
        yield return new WaitForSeconds(1.5f);
        if (!dataController.getisEditig())
        {
            dataController.setCurrentRound(data);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
        else
        {
            dataController.setCurrentRound(data);
            UnityEngine.SceneManagement.SceneManager.LoadScene("CreateQuestions");
        }
    }


    void showQuizList()
    {

    }

    void ShowQuestion()
    {                                      

        for (int i = 0; i < roundList.Length; i++)                               // For every AnswerData in the current QuestionData...
        {
            GameObject quizButtonGameObject = quizbutonObjectPool.GetObject();         // Spawn an AnswerButton from the object pool
            answerButtonGameObjects.Add(quizButtonGameObject);
            quizButtonGameObject.transform.SetParent(quizButtonParent);
            quizButtonGameObject.transform.localScale = Vector3.one;

            QuizButton answerButton = quizButtonGameObject.GetComponent<QuizButton>();
            answerButton.SetUp(roundList[i]);                                    // Pass the AnswerData to the AnswerButton so the AnswerButton knows what text to display and whether it is the correct answer
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
