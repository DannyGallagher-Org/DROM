///
// DromCamera - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class DromCamera : MonoBehaviour {

	#region private variables
    private readonly float[] _weights = new float[]
    {
        3f,
        0f
    };

    private int _timeOfDayCount = 0;

	private AmplifyColorEffect _amp;
    private const float DayChangeSpeed = 1f;

    public float TargetColorSetting = 0f;
	#endregion

	#region monobehaviour inherited
	void Awake () {
		_amp = GetComponent<AmplifyColorEffect> ();
	}

	void Update() {
		if (!Mathf.Approximately (_amp.BlendAmount, TargetColorSetting))
			_amp.BlendAmount = Mathf.Lerp (_amp.BlendAmount, TargetColorSetting, Time.deltaTime * DayChangeSpeed);
	}
	#endregion

	#region public methods
	public void MoveTimeForward()
	{
	    _timeOfDayCount++;
	    TargetColorSetting = _weights[_timeOfDayCount];
	}
	#endregion

	#region event handlers
	#endregion
}
