/*===============================================================
Product:    Drom
Developer:  Danny Gallagher
Company:    House of Wire
Date:       18/10/16
================================================================*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

    #region typedefs
    enum Quality
    {
        Fastest,
        Fast,
        Simple,
        Good,
        Beautiful,
        Fantastic
    }

    Resolution[] resolutions;
    #endregion

    #region events and delegates
    public delegate void MenuEventHandler();
    public event MenuEventHandler MenuReadyEvent;
    public event MenuEventHandler StartGameEvent;
    #endregion

    #region public
    public Text[] choices;
    public Color otherColor;
    #endregion

    #region private variables
    private int _selection = 0;
    private Animator _animator;

    private int _resolution = 0;
    private int _quality = 0;
    private bool _bInUse = false;
    private bool _bStartingGame = false;
    #endregion

    #region MonoBehaviour inherited
    // Use this for initialization
    void Awake()
    {
        resolutions = Screen.resolutions;
        _animator = GetComponent<Animator>();
        choices[_selection].color = Color.white;
        _quality = QualitySettings.GetQualityLevel();

        choices[1].text = "Resolution : " + Screen.currentResolution;

        for (int i = 0; i<resolutions.Length-1;i++)
        {
            if (Screen.currentResolution.ToString() == resolutions[i].ToString())
            {
                _resolution = i;
                break;
            }
        }

        choices[2].text = "Quality : " + ((Quality)_quality).ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_bInUse)
                AnimateOff();
            else
                AnimateOn();
        }

        if (!_bInUse) return;

        if(Input.GetKeyDown(KeyCode.Return))
            Select();

        if(Input.GetKeyDown(KeyCode.UpArrow))
            SelectUp();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            SelectDown();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SelectLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            SelectRight();
    }
    #endregion

    #region public
    public void ToggleUseMode(bool toggle)
    {
        _bInUse = toggle;

        if(_bInUse)
            choices[_selection].color = Color.white;
    }

    public void AnimateOn()
    {
        _quality = QualitySettings.GetQualityLevel();
        choices[2].text = "Quality : " + ((Quality)_quality).ToString();

        _animator.SetTrigger("tAnimateOn");
        Invoke("DidAnimateOn", 1.5f);
    }

    public void AnimateOff()
    {
        _bInUse = false;
        _animator.enabled = true;
        _animator.Play("AnimateOff");
        Invoke("DidAnimateOff", 1.5f);
    }

    public void DidAnimateOn()
    {
        _animator.enabled = false;
        choices[_selection].color = Color.white;
        _bInUse = true;

        if (MenuReadyEvent != null)
            MenuReadyEvent();
    }

    public void DidAnimateOff()
    {
        if (_bStartingGame && StartGameEvent != null)
            StartGameEvent();
    }
    #endregion

    #region private methods
    void Select()
    {
        if (_selection == 0)
        {
            _bStartingGame = true;
            AnimateOff();
        }

		GameManager.audioManager.PlaySFX ("menu_sfx");

		Debug.Log ("menu select");
//		GameManager.audioManager.MenuSelect ();
		GameManager.audioManager.PlayMenuClickSelect ();
    }

    void SelectUp()
    {
        choices[_selection].color = otherColor;

        if (_selection > 0)
            _selection--;
        else
            _selection = 2;

        choices[_selection].color = Color.white;

		Debug.Log ("menu select up");
//		GameManager.audioManager.MenuMove ();
		GameManager.audioManager.PlayMenuClickUp ();
    }

    void SelectDown()
    {
        choices[_selection].color = otherColor;

        if (_selection < 2)
            _selection++;
        else
            _selection = 0;

        choices[_selection].color = Color.white;

		Debug.Log ("menu select down");
//		GameManager.audioManager.MenuMove ();
		GameManager.audioManager.PlayMenuClickDown ();
    }

    void SelectLeft()
    {
        switch(_selection)
        {
            case 1:
                if(_resolution > 0)
                {
                    _resolution--;
                    Resolution res = resolutions[_resolution];
                    Screen.SetResolution(res.width, res.height, Screen.fullScreen);
                    choices[1].text = "Resolution : " + Screen.currentResolution;
                }
                break;

            case 2:
                if (_quality > 0)
                {
                    _quality--;
                    QualitySettings.DecreaseLevel();
                    choices[2].text = "Quality : " + ((Quality)_quality).ToString();
                }
                break;
        }

		Debug.Log ("menu move left");
//		GameManager.audioManager.MenuMove ();
		GameManager.audioManager.PlayMenuClickSelect ();
    }

    void SelectRight()
    {
        switch(_selection)
        {
            case 1:
                if(_resolution < resolutions.Length-1)
                {
                    _resolution++;
                    Resolution res = resolutions[_resolution];
                    Screen.SetResolution(res.width, res.height, Screen.fullScreen);
                    choices[1].text = "Resolution : " + Screen.currentResolution;
                }
                break;

            case 2:
                if(_quality < (int)Quality.Fantastic)
                {
                    _quality++;
                    QualitySettings.IncreaseLevel();
                    choices[2].text = "Quality : " + ((Quality)_quality).ToString();
                }
                break;
        }

		Debug.Log ("menu move right");
//		GameManager.audioManager.MenuMove ();
		GameManager.audioManager.PlayMenuClickSelect ();
    }
    #endregion
}
