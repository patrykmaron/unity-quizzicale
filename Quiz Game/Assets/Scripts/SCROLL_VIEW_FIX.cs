using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCROLL_VIEW_FIX : MonoBehaviour {

    // THE CAMERA HAS TO START AS DEFAULT SIZE
    void Start() {
        Camera.main.orthographicSize = 4; // Default size to start with
    }

	

    // THEN FOR SCROLL BUTTONS TO SPAWN, THE SIZE HAS TO UPDATED DOWN --- WHY? I HAVE NO IDEA, BUT IT WORKS <3 Pat
    void Update () {

            if(Camera.main.orthographicSize == 4)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - 1 * Time.deltaTime;
            if (Camera.main.orthographicSize > 3.33)
            {
                Camera.main.orthographicSize = 3;
            }
        }
		
	}
}
