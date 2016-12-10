///
// CloudController - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

    #region events and delegates
    public delegate void CloudMoveEventHandler();
    public event CloudMoveEventHandler CloudMoveFinishedEvent;
    #endregion

    #region private variables
    private Vector3 _startPos;
	private Vector3 _lastPos;

	private bool _bMove = false;
	private bool _bMoveOff = false;
	#endregion

	#region public interface
	public GameObject cloudClone;
	public float speed = 35f;
	#endregion

	#region monobehaviour inherited
	void Awake() {
		_startPos = transform.position;
		_lastPos = _startPos;
	}

	void Update () {

	}
	#endregion

	#region public methods
	public void Move() {
        Go.to(transform, 10f, new GoTweenConfig()
            .localPosition(new Vector3(0, 0, 7f))
            .setEaseType(GoEaseType.Linear)
            .onComplete(CloudOnComplete)
            );
	}

	public void MoveOff() {
		_bMoveOff = true;
	}
	#endregion

	#region private methods
    void CloudOnComplete(AbstractGoTween t)
    {
        if (CloudMoveFinishedEvent != null)
            CloudMoveFinishedEvent();
    }

    void CloudOffComplete()
    {

    }
	#endregion

	#region event handlers
	#endregion
}
