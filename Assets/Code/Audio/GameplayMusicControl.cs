using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicControl : MonoBehaviour {

	public static FMODUnity.StudioEventEmitter emitter;
	bool playOnce = true;

	// Use this for initialization
	void Start () {
		emitter = GetComponent<FMODUnity.StudioEventEmitter>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		emitter.SetParameter ("level_01", Boss.ratio);
		Debug.Log ("ratio = " + Boss.ratio);
		
	}


	public void PlayMusic (){

		if (playOnce) {

			emitter.Play();
			Debug.Log ("PlayMusic() called");
			playOnce = false;
		}

	}
}
