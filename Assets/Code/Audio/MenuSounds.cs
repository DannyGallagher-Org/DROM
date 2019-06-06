using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour {

	public AudioClip menuMove;
	public AudioClip menuSelect;
	public AudioClip menuclick_down;
	public AudioClip menuclick_up;
	public AudioClip menuclick_select;


	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.Z)){
			PlayMenuClick("down");
		}
		if (Input.GetKeyDown(KeyCode.X)){
			PlayMenuClick("up");
		}
		if (Input.GetKeyDown(KeyCode.C)){
			PlayMenuClick("select");
		}
		
	}



	public void PlayMenuMoveSound (){

		audioSource.PlayOneShot (menuMove, 0.7f);
//		Debug.Log ("PlayMenuMoveSound attemtped");
	}

	public void PlayMenuSelectSound (){

		audioSource.PlayOneShot (menuSelect, 0.7f);
		//		Debug.Log ("PlayMenuMoveSound attemtped");
	}


	public void PlayMenuClick (string type){
	
		AudioClip clip = null;

		if (type == "down")
			clip = menuclick_down;
		else if (type == "up")
			clip = menuclick_up;
		else if (type == "select") clip = menuclick_select;

		if(clip != null)
			audioSource.PlayOneShot (clip, 1f);
	}
}
