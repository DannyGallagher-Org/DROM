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
	private int _level = 0;

    private CloudController _currentCloud;
    #endregion

    #region public interface
    public Menu menu;

	public ShapeCheckCamera shapeCheckCam;
	public DromCamera dcamera;

    public static AudioManagerClass audioManager;

	public GameObject text;

	public GameObject[] matchObj;

    public float startRed;
    public float ratioDebug = 0;
	public static float ratio = 0f;

	public float[] targets;

    private GameObject _stage;
	private Intro intro;
    #endregion

    #region monobehaviour inherited
    void Awake () {
        audioManager = gameObject.AddComponent<AudioManagerClass> ();
		intro = GameObject.FindObjectOfType<Intro> ();
		if(GameDefs.kSpeedyIntro)
        {
            Menu_StartGameEvent();
        }
        else
        {
			intro.IntroCompleteEvent += Boss_IntroCompleteEvent;
			intro.TitleShowEvent += TitleShowEvent;
        }
	}

    void TitleShowEvent ()
	{
		GameObject.FindObjectOfType<Intro>().TitleShowEvent -= TitleShowEvent;
		DromReveal.DromRevealPlay ();
    }

    void Update() {
		switch (_state) {
		case State.Intro:
			    // Wait
			break;

		case State.Start:
			GameManager.audioManager.PlayGameplayMusic ();
                NextCloud();
            break;

		case State.CloudMove:
			
			break;

		case State.Guess:
                float tot = (shapeCheckCam.blue-shapeCheckCam.red) / startRed;

                ratioDebug = ratio = Mathf.Lerp(ratio, Mathf.Clamp(tot, 0, 1f), Time.deltaTime);

                if (ratio > targets[_level]) {
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
		GameManager.audioManager.PlayWinSound ();
		dcamera.MoveTimeForward ();
		if (_level + 1 > targets.Length - 1) {

			if (!_bEnded) {

				_bEnded = true;

				Invoke ("Quit", 120f);
			}
		} else {
            GameObject.Destroy(_stage);
            NextCloud();
            ratio = 0;
			GameManager.audioManager.NextLevel ();
        }
	}
    #endregion

    #region private methods
    private void NextCloud()
    {
        _stage = GameObject.Instantiate(Resources.Load(string.Format("Stage_{0}", _level)) as GameObject);

		_currentCloud = GameObject.Instantiate(Resources.Load("GameCloud" + _level) as GameObject).GetComponent<CloudController>();

        _currentCloud.CloudMoveFinishedEvent += _currentCloud_CloudMoveFinishedEvent;
        _currentCloud.Move();

        _state = State.CloudMove;
    }

    private void _currentCloud_CloudMoveFinishedEvent()
    {
        _currentCloud.CloudMoveFinishedEvent -= _currentCloud_CloudMoveFinishedEvent;
        startRed = shapeCheckCam.Check(true).y;
        _state = State.Guess;
    }

    private void Boss_IntroCompleteEvent()
	{
		GameObject.FindObjectOfType<Intro>().IntroCompleteEvent -= Boss_IntroCompleteEvent;
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
		menu.StartGameEvent -= Menu_StartGameEvent;
		dcamera.targetColorSetting = 0.3f;
        _state = State.Start;
    }

    public void Begin()
    {
        _state = State.Start;
    }
    #endregion
}
