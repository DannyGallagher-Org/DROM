using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
	//managers
	public static AudioManager audioManager; 

	// Use this for initialization
	void Awake () {
        Debug.Log("HEY");
		audioManager = gameObject.AddComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
