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
	#endregion

	#region monobehaviour inherited
	void Awake () {
		amp = GetComponent<AmplifyColorEffect> ();
	}

	void Update() {
		if (_bStarted) {
			if (amp.BlendAmount > 0 && _bStarted)
				amp.BlendAmount -= dayChangeSpeed * (Time.deltaTime / 30f);

			if (transform.position.y > 6.4f) {
				Vector3 pos = transform.position;
				float speed = transform.position.y - 6.4f;
				pos.y -= Time.deltaTime * Mathf.Clamp (speed, 0.01f, 0.3f);

				transform.position = pos;
			}
		}

		if (_bFinished) {
			if (transform.position.y < 10f) {
				Vector3 pos = transform.position;
				float speed = 10f - transform.position.y;
				pos.y += Time.deltaTime * Mathf.Clamp (speed, 0.01f, 0.1f);

				transform.position = pos;
			}
		}
			
	}
	#endregion

	#region private methods
	#endregion

	#region event handlers
	#endregion
}
