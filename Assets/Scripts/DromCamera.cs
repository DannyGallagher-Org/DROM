///
// DromCamera - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class DromCamera : MonoBehaviour {

	#region private variables
	private AmplifyColorEffect amp;
	private float dayChangeSpeed = 0.1f;

	private bool _bStarted = false;
	private bool _bFinished = false;

	public float targetColorSetting = 0f;
	#endregion

	#region monobehaviour inherited
	void Awake () {
		amp = GetComponent<AmplifyColorEffect> ();
	}

	void Update() {
		if (!Mathf.Approximately (amp.BlendAmount, targetColorSetting))
			amp.BlendAmount = Mathf.Lerp (amp.BlendAmount, targetColorSetting, Time.deltaTime);
	}
	#endregion

	#region private methods
	#endregion

	#region event handlers
	#endregion
}
