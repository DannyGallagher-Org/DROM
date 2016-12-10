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

	private State _state = State.Intro;
	private int _level = 1;

    private CloudController _currentCloud;
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

    public float startRed;
    public float ratioDebug = 0;
	public static float ratio = 0f;

	public float[] targets;

    private GameObject _stage;

    private GameplayMusicControl _gamePlayMusic;
    #endregion

    #region monobehaviour inherited
    void Awake () {
        _gamePlayMusic = FindObjectOfType<GameplayMusicControl>();
        audio = gameObject.AddComponent<AudioManagerClass> ();
        if(GameDefs.kSpeedyIntro)
        {
            GameObject.FindObjectOfType<Canvas>().gameObject.SetActive(false);
            Menu_StartGameEvent();
        }
        else
        {
            GameObject.FindObjectOfType<Intro>().IntroCompleteEvent += Boss_IntroCompleteEvent;
        }
	}

    void Update() {
		switch (_state) {
		case State.Intro:
			    // Wait
			break;

		case State.Start:
                dcamera.Move();
                _gamePlayMusic.PlayMusic();
                NextCloud();
            break;

		case State.CloudMove:
			    guy.gameObject.SetActive (true);
			    guyopen.gameObject.SetActive (false);
			
			break;

		case State.Guess:
                float tot = (shapeCheckCam.blue-shapeCheckCam.red) / startRed;

                ratioDebug = ratio = Mathf.Lerp(ratio, Mathf.Clamp(tot, 0, 1f), Time.deltaTime);

                if (ratio > targets[_level-1]) {
				    Win();
			    }
				
			break;
		}
	}
    #endregion

    #region public methods
	public void Win() {
        Debug.Log(_currentCloud.gameObject);
        GameObject.Destroy(_currentCloud.gameObject);
        Debug.Log("win");
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
            GameObject.Destroy(_stage);
            NextCloud();
            ratio = 0;
            _gamePlayMusic.NextLevel();
        }
	}
    #endregion

    #region private methods
    private void NextCloud()
    {
        _stage = GameObject.Instantiate(Resources.Load(string.Format("Stage_{0}", _level)) as GameObject);

        _currentCloud = GameObject.Instantiate(Resources.Load("NiceCloud1") as GameObject).GetComponent<CloudController>();

        _currentCloud.CloudMoveFinishedEvent += _currentCloud_CloudMoveFinishedEvent;
        _currentCloud.Move();

        _state = State.CloudMove;
    }

    private void _currentCloud_CloudMoveFinishedEvent()
    {
        _currentCloud.CloudMoveFinishedEvent -= _currentCloud_CloudMoveFinishedEvent;
        startRed = shapeCheckCam.Check().y;
        _state = State.Guess;
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
