using UnityEngine;

public class MenuScreenController : MonoBehaviour
{
	public void StartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}

    public void CreateGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CreateGame");

    }

    public void openLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    public void openQuizSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("QuizSelect");
    }

    public void editQuiz()
    {

        DataController dataController = FindObjectOfType<DataController>();
        dataController.isEditingSet(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("QuizSelect");

      
    }
}