using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour {

    public static GlobalObject instance;

    public static RoundData addRoundData;

	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
	}
	

    
}
