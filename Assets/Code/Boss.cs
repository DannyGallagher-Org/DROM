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
		Start,
		CloudMove,
		Guess,
		Win,
		Complete
	}

	#region private variables
	private bool _bEnded = false;

    private static Vector3 kCloudPos = new Vector3(178.9977f, 105.5037f, 268.0649f);

	private State _state = State.Intro;
	private int _level = 1;

    private CloudController _currentCloud;
    private Thoughts _currentThought;
    #endregion

    #region public interface
    public Menu menu;

	public ShapeCheckCamera shapeCheckCam;
	public DromCamera dcamera;

	public static AudioManagerClass audio;

	public GameObject text;

	public SpriteRenderer guy;
	public SpriteRenderer guyopen;
	public GameObject[] shapes;
	public GameObject[] matchObj;
    
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
                dcamera.Move();

                NextCloud();
                break;

		case State.CloudMove:
			
			startRed = shapeCheckCam.red;

			guy.gameObject.SetActive (true);
			guyopen.gameObject.SetActive (false);
			if (_currentCloud.transform.position.x <= 130f) {
				_currentThought.Show ();
				_state = State.Guess;
			}
			break;

		case State.Guess:

			Invoke ("OpenEyes", 15f);
			float targ = ((Screen.width * Screen.height) / 100) * targets [_level];

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
		if (_level + 1 > targets.Length - 1) {

			if (!_bEnded) {
				audio.PlaySFX (Resources.Load ("third") as AudioClip);
				_bEnded = true;
				dcamera.Finish ();
                /*
				clouds [level].MoveOff ();

				clouds [3].speed = 6f;
				clouds [4].speed = 8f;
				clouds [5].speed = 10f;
				clouds [3].MoveOff ();
				clouds [4].MoveOff ();
				clouds [5].MoveOff ();
                */
				shapes [_level].SetActive (false);

				Invoke ("Quit", 120f);
			}
		} else {
            _currentCloud.MoveOff();
            NextCloud();

            /*
			clouds [level].MoveOff ();
			shapes [level].SetActive (false);
			matchObj [level].SetActive (false);
			level++;
			matchObj [level].SetActive (true);
			clouds [level].Move ();
			_state = State.CloudMove;
            */
		}
	}
    #endregion

    #region private methods
    private void NextCloud()
    {
        GameObject cloudGO = GameObject.Instantiate(Resources.Load(string.Format("Stage_{0}", _level)) as GameObject);
        cloudGO.transform.position = kCloudPos;

        _currentCloud = cloudGO.transform.FindChild("Cloud").GetComponent<CloudController>();
        _currentThought = cloudGO.transform.FindChild("Thoughts").GetComponent<Thoughts>();

        _currentCloud.Move();
        _state = State.CloudMove;
    }

    private void Boss_IntroCompleteEvent()
    {
        menu.MenuReadyEvent += Menu_MenuReadyEvent;
        menu.AnimateOn();
    }

    private void Menu_MenuReadyEvent()
    {
        menu.MenuReadyEvent -= Menu_MenuReadyEvent;
        menu.StartGameEvent += Menu_StartGameEvent;
    }

    private void Menu_StartGameEvent()
    {
        _state = State.Start;
    }

    public void Begin()
    {
        _state = State.Start;
    }
    #endregion
}
