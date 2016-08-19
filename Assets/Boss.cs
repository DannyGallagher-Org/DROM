///
// Boss - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	private enum State
	{
		Start,
		Intro,
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

	public float[] targets;
	#endregion

	#region monobehaviour inherited
	void Awake () {
		audio = gameObject.AddComponent<AudioManagerClass> ();
	}

	void Update() {

		switch (_state) {

		case State.Start:
			if (Input.GetKey (KeyCode.Escape))
				Quit ();

				if (Input.anyKey) {
					clouds [level].Move ();
					text.AddComponent<TextFade> ();
					dcamera.Move ();
					_state = State.CloudMove;
				}
				break;

		case State.CloudMove:
			if (Input.GetKey (KeyCode.Escape))
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			
			guy.gameObject.SetActive (true);
			guyopen.gameObject.SetActive (false);
			if (clouds [level].transform.position.x < 275f) {
				thoughts [level].Show ();
				_state = State.Guess;
			}
			break;

		case State.Guess:
			if (Input.GetKey (KeyCode.Escape))
				UnityEngine.SceneManagement.SceneManager.LoadScene (0);

			Invoke ("OpenEyes", 15f);
			float targ = ((Screen.width * Screen.height) / 100 )* targets [level];
			if (shapeCheckCam.red <  targ) {
//			if(Input.GetKeyDown(KeyCode.Space)){
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

	public void Quit() {
		Application.Quit ();
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
	#endregion

	#region event handlers
	#endregion
}
