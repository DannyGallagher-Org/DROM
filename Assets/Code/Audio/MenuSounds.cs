using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;

public class MenuSounds : MonoBehaviour {

	public AudioClip menuMove;
	public AudioClip menuSelect;


	AudioSource audio;
//	AudioClip [] menuSounds;

	// Use this for initialization
	void Start () {

//		menuSounds = GetComponents<AudioClip> ();
		audio = GetComponent<AudioSource> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void PlayMenuMoveSound (){

		audio.PlayOneShot (menuMove, 0.7f);
//		Debug.Log ("PlayMenuMoveSound attemtped");
	}

	public void PlayMenuSelectSound (){

		audio.PlayOneShot (menuSelect, 0.7f);
		//		Debug.Log ("PlayMenuMoveSound attemtped");
	}
}
