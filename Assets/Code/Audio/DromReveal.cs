using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DromReveal : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource> ();

		Invoke ("DromRevealPlay", 21f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.P)) {
			
			DromRevealPlay ();
		
		}
		
	}

	public void DromRevealPlay () {

		audioSource.Play();
		Debug.Log ("Drom Reveal Play attemtped");
	}

}
