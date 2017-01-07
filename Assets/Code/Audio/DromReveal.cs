using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DromReveal : MonoBehaviour {

	public static AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void DromRevealPlay () {

		audioSource.Play();
		Debug.Log ("Drom Reveal Play attemtped");
	}

}
