﻿/*===============================================================
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
        if (!_bInUse) return;

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
        _animator.Play(0);
        _animator.SetTrigger("tAnimateOff");
        Invoke("DidAnimateOff", 1.5f);
    }

    public void DidAnimateOn()
    {
        _animator.Stop();
        choices[_selection].color = Color.white;
        _bInUse = true;

        if (MenuReadyEvent != null)
            MenuReadyEvent();
    }

    public void DidAnimateOff()
    {
        _bInUse = false;
    }
    #endregion

    #region private methods
    void SelectUp()
    {
        choices[_selection].color = otherColor;

        if (_selection > 0)
            _selection--;
        else
            _selection = 2;

        choices[_selection].color = Color.white;
    }

    void SelectDown()
    {
        choices[_selection].color = otherColor;

        if (_selection < 2)
            _selection++;
        else
            _selection = 0;

        choices[_selection].color = Color.white;
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
    }
    #endregion
}
