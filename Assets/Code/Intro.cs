/*===============================================================
Product:    Drom
Developer:  Danny Gallagher
Company:    House of Wire
Date:       18/10/16
================================================================*/

using UnityEngine;

public class Intro : MonoBehaviour {

    #region delegates and events
    public delegate void IntroCompleteEventHandler();
	public event IntroCompleteEventHandler IntroCompleteEvent;
	public event IntroCompleteEventHandler TitleShowEvent;
    #endregion

    #region private variables
    private Animator _animator;
	#endregion

	#region MonoBehaviour inherited
	// Use this for initialization
	void Awake () {
        _animator = GetComponent<Animator>();
        
        if (GameDefs.kSpeedyIntro)
            _animator.speed = 10f;
    }
#endregion

#region public methods
	public void ShowTitle()
	{
		
	}

    public void EndIntro()
    {
        _animator.Stop();

        if (IntroCompleteEvent != null)
            IntroCompleteEvent();
    }
#endregion
}
