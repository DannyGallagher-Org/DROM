///
// Boss - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	private enum State
	{
		Intro,
        StartMenu,
		Start,
		CloudMove,
		Guess,
		Win,
		Complete
	}

	#region private variables
	private bool _bEnded = false;

	private State _state = State.Start;
	public int level = 0;
	#endregion

	#region public interface
	public GameObject logo;
	public GameObject titleText;
	public Image startOverlay;

    public Menu menu;

	public ShapeCheckCamera shapeCheckCam;
	public DromCamera dcamera;

	public static AudioManagerClass audio;

	public GameObject text;

	public SpriteRenderer guy;
	public SpriteRenderer guyopen;

	public CloudController[] clouds;
	public GameObject[] shapes;
	public GameObject[] matchObj;
	public Thoughts[] thoughts;
    
	public static float ratio = 0f;
	public float startRed = 0f;

	public float[] targets;
	#endregion

	#region monobehaviour inherited
	void Awake () {
		audio = gameObject.AddComponent<AudioManagerClass> ();
        GameObject.FindObjectOfType<Intro>().IntroCompleteEvent += Boss_IntroCompleteEvent;
	}

    void Update() {

		switch (_state) {
		case State.Intro:
			    // Wait
			break;

		case State.Start:
			if (Input.GetKeyDown(KeyCode.Return)) {
				titleText.GetComponent<TextFade> ().target = 0f;

				dcamera.Move ();
					
				clouds [level].Move ();
				_state = State.CloudMove;
			}
			break;

		case State.CloudMove:
			
			startRed = shapeCheckCam.red;

			guy.gameObject.SetActive (true);
			guyopen.gameObject.SetActive (false);
			if (clouds [level].transform.position.x <= 260f) {
				thoughts [level].Show ();
				_state = State.Guess;
			}
			break;

		case State.Guess:

			Invoke ("OpenEyes", 15f);
			float targ = ((Screen.width * Screen.height) / 100) * targets [level];

			ratio = 1-(((shapeCheckCam.red - targ) / ((startRed - targ) / 100f)) * (1/100f));

			if (shapeCheckCam.red <  targ) {
				Win();
			}
				
			break;
		}
	}
    #endregion

    #region public methods
	public void OpenEyes() {
		guy.gameObject.SetActive (false);
		guyopen.gameObject.SetActive (true);
		CancelInvoke ("OpenEyes");
	}

	public void Win() {
		if (level + 1 > targets.Length - 1) {

			if (!_bEnded) {
				audio.PlaySFX (Resources.Load ("third") as AudioClip);
				_bEnded = true;
				dcamera.Finish ();
				clouds [level].MoveOff ();

				clouds [3].speed = 6f;
				clouds [4].speed = 8f;
				clouds [5].speed = 10f;
				clouds [3].MoveOff ();
				clouds [4].MoveOff ();
				clouds [5].MoveOff ();

				shapes [level].SetActive (false);

				Invoke ("Quit", 120f);
			}
		} else {
			if (level == 0)
				audio.PlaySFX (Resources.Load ("first") as AudioClip);
			else if (level == 1)
				audio.PlaySFX (Resources.Load ("second") as AudioClip);

			clouds [level].MoveOff ();
			shapes [level].SetActive (false);
			matchObj [level].SetActive (false);
			level++;
			matchObj [level].SetActive (true);
			clouds [level].Move ();
			_state = State.CloudMove;
		}
	}
    #endregion

    #region private methods
    private void Boss_IntroCompleteEvent()
    {
        menu.AnimateOn();
    }

    public void Begin()
    {
        _state = State.Start;
    }
    #endregion
}
