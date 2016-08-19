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

	#region public interface
	public ShapeCheckCamera shapeCheck;

	public int[] targets;

	public CloudController[] clouds;
	public Thoughts[] thoughts;
	#endregion

	#region monobehaviour inherited
	void Awake () {
		amp = GetComponent<AmplifyColorEffect> ();

		Vector3 pos = transform.position;
		pos.y = 8f;

		transform.position = pos;
	}

	void Update() {

		if (_bStarted) {
			if (amp.BlendAmount > 0.25 && _bStarted)
				amp.BlendAmount -= dayChangeSpeed * (Time.deltaTime / 2f);

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

	#region public methods
	public void Move() {
		_bStarted = true;
	}

	public void Finish() {
		_bStarted = false;
		_bFinished = true;
	}
	#endregion

	#region private methods
	#endregion

	#region event handlers
	#endregion
}
