using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private GameplayMusicControl _gameplayMusicControl;


	void Awake (){
		_gameplayMusicControl = FindObjectOfType<GameplayMusicControl> ();
	}




	public void PlaySFX (string sfx){
	
	}

	public void PlayGameplayMusic (){

		_gameplayMusicControl.PlayGameplayMusic ();

	}

	public void NextLevel (){
	
	
		_gameplayMusicControl.NextLevel ();
	
	}


}
