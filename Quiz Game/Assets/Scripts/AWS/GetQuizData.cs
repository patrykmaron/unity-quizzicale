using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetQuizData{
    public string QuizName;
    public int QuizID;

    public static GetQuizData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GetQuizData>(jsonString);
    }
}
