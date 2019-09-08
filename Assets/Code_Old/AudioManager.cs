using UnityEngine;

public class AudioManager : MonoBehaviour {

	private GameplayMusicControl _gameplayMusicControl;
	private MenuSounds _menuSounds;
	private CloudBitSounds _cloudBitSounds;
	private WinSound _winSound;


	void Awake (){
		_gameplayMusicControl = FindObjectOfType<GameplayMusicControl> ();
		_menuSounds = FindObjectOfType<MenuSounds> ();
		_cloudBitSounds = FindObjectOfType<CloudBitSounds> ();
		_winSound = FindObjectOfType<WinSound> ();
	}





	public void PlaySFX (string sfx){
	
	}

	public void PlayGameplayMusic (){

		_gameplayMusicControl.PlayGameplayMusic ();

	}

	public void NextLevel (){
	
		_gameplayMusicControl.NextLevel ();
	
	}


	public void DromReveal () {

		//currently invoked by DromReveal script

	}

	public void MenuMove () {

		_menuSounds.PlayMenuMoveSound();
	}

	public void MenuSelect () {

		_menuSounds.PlayMenuSelectSound();
	}

	public void PlayCloudBitSound(){
	
		_cloudBitSounds.PlayCloudBitSound ();

	}

	public void PlayWinSound(){
	
		_winSound.PlayWinSound ();
	
	}

	public void PlayMenuClickDown(){
		_menuSounds.PlayMenuClick ("down");
	}
	public void PlayMenuClickUp(){
		_menuSounds.PlayMenuClick ("up");
	}
	public void PlayMenuClickSelect(){
		_menuSounds.PlayMenuClick ("select");
	}

	//TODO make 'GameplayMusicTransition (int transNumber) {}... pass the transition number
	public void GameplayMusicTransitionOne () {

		_gameplayMusicControl.TransitionOne ();

	}
}
