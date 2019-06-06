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

	private bool _bMoveOff = false;
	#endregion

	#region public interface
	public GameObject cloudClone;
	public float speed = 35f;

    public float opacity;
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
        float time = (GameDefs.kSpeedyGame) ? 1f : 30f;

        Go.to(transform, time, new GoTweenConfig()
            .localPosition(new Vector3(0, 0, 4f))
            .setEaseType(GoEaseType.Linear)
            .onComplete(CloudOnComplete)
            );
	}

	public void MoveOff(float time) {
        time = (GameDefs.kSpeedyGame) ? 1f : time;

        Go.to(transform, time, new GoTweenConfig()
            .localPosition(new Vector3(30f, 0, 4f))
            .setEaseType(GoEaseType.Linear)
            .onComplete(CloudOffComplete)
            );
    }
	#endregion

	#region private methods
    void CloudOnComplete(AbstractGoTween t)
    {
        if (CloudMoveFinishedEvent != null)
            CloudMoveFinishedEvent();
    }

    void CloudOffComplete(AbstractGoTween t)
    {
        t.destroy();
        Destroy(gameObject);
    }
	#endregion

	#region event handlers
	#endregion
}
