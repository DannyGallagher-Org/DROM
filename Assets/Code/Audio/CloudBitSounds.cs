using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBitSounds : MonoBehaviour {

	public AudioClip[] cloudSounds;

	AudioSource audio;
	int idx;
	float pitch;
	float vol;


	// Use this for initialization
	void Awake () {

//		cloudSounds = GetComponents<AudioClip> ();
		audio = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C))
			PlayCloudBitSound ();
		
	}


	public void PlayCloudBitSound (){
	
	

		idx = Random.Range(0, 14); //15 clips in array
		pitch = Random.Range (0.75f, 1.25f);
		vol = Random.Range (0.75f, 1f);

		audio.pitch = pitch;
		audio.PlayOneShot (cloudSounds [idx], vol);
	
	
	}
}
