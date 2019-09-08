using UnityEngine;

public class GameplayMusicControl : MonoBehaviour {

	public static FMODUnity.StudioEventEmitter emitter;
	bool playOnce = true;

    private string _currentParam;
    private int _currentLevel = 0;

    private string[] parameters = new string[]
    {
        "level_01",
        "level_02",
        "level_03",
        "level_04",
        "level_05",
    };

	// Use this for initialization
	void Start () {
        _currentParam = parameters[_currentLevel];
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		emitter.SetParameter (_currentParam, Boss.ratio);
	}

    public void NextLevel()
    {
        _currentLevel++;
        _currentParam = parameters[_currentLevel];
    }

	public void PlayGameplayMusic (){

		if (playOnce) {

			emitter.Play();
			Debug.Log ("PlayMusic() called");
			playOnce = false;
		}

	}

	public void TransitionOne () {

		emitter.SetParameter ("trans_01", 1f);
		Debug.Log ("tried to set trans_01 parameter");

	}
}
