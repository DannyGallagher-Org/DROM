using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSound : MonoBehaviour {

	AudioSource audio;

	// Use this for initialization
	void Awake () {

		audio = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.A))
			PlayWinSound ();
		
	}

	public void PlayWinSound(){
	
	
		audio.Play ();
	
	}
}
