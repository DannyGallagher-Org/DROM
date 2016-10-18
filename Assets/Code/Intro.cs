/*===============================================================
Product:    #PROJECTNAME#
Developer:  #DEVELOPERNAME#
Company:    #COMPANY#
Date:       #CREATIONDATE#
================================================================*/

using UnityEngine;

public class Intro : MonoBehaviour {

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
    public void EndIntro()
    {
        _animator.Stop();
    }
    #endregion
}
