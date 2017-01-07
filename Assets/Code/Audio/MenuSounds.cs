using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;

public class MenuSounds : MonoBehaviour {

	public AudioClip menuMove;
	public AudioClip menuSelect;
	public AudioClip menuclick_down;
	public AudioClip menuclick_up;
	public AudioClip menuclick_select;


	AudioSource audio;
//	AudioClip [] menuSounds;

	// Use this for initialization
	void Start () {

//		menuSounds = GetComponents<AudioClip> ();
		audio = GetComponent<AudioSource> ();

		
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

		audio.PlayOneShot (menuMove, 0.7f);
//		Debug.Log ("PlayMenuMoveSound attemtped");
	}

	public void PlayMenuSelectSound (){

		audio.PlayOneShot (menuSelect, 0.7f);
		//		Debug.Log ("PlayMenuMoveSound attemtped");
	}


	public void PlayMenuClick (string type){
	
		AudioClip clip = new AudioClip();

		switch (type) {

		case "down":
			clip = menuclick_down;
			break;
		case "up":
			clip = menuclick_up;
			break;
		case "select":
			clip = menuclick_select;
			break;
//		default:
//			menuClick = menuclick_down;

		}

	
		audio.PlayOneShot (clip, 1f);
	
	
	}
}
