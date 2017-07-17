using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressSpace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
	    {
	        if(GameDefs.kbTrailer)
                SceneManager.LoadScene(2);
            else
                SceneManager.LoadScene(1);
	    }
	}
}
