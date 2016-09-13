///
// CloudController - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {

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

		if (_bMoveOff) {
			if (transform.position.x > -500f) {
				Vector3 newPos = transform.position;

				newPos.x -= Time.deltaTime * speed;

				transform.position = newPos;
			}
		}

		if (_bMove) {
			if (transform.position.x > 260f) {
				Vector3 newPos = transform.position;

				float speed = transform.position.x - 260f;
				newPos.x -= Time.deltaTime * Mathf.Clamp (speed, 1f, 40f);

				transform.position = newPos;
			}
		}
	}
	#endregion

	#region public methods
	public void Move() {
		_bMove = true;
	}

	public void MoveOff() {
		_bMoveOff = true;
	}
	#endregion

	#region private methods
	#endregion

	#region event handlers
	#endregion
}
