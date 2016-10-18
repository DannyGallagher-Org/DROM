///
// CloudBit - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class CloudBit : MonoBehaviour {

    #region private variables
    private Rigidbody _rigidBody;
    private Vector2 _startClick;
    private Vector2 _nowClick;

    private bool _bClicked;
	#endregion

	#region public interface
	public float speed = 20f;
	#endregion

	#region monobehaviour inherited
	void Awake () {
        _rigidBody = GetComponent<Rigidbody>();
    }

	void Update() {
        Vector3 velocity = _rigidBody.velocity;
        velocity.z = 0f;
        _rigidBody.velocity = velocity;

        if(Vector2.Distance(_startClick, _nowClick) > 2f)
        {
            Move();
        }
    }
	#endregion

	#region public methods
    public void Click(Vector2 click)
    {
        if (!_bClicked)
        {
            _startClick = click;
            _bClicked = true;
        }
        else
            _nowClick = click;
    }
	#endregion

	#region private methods
	void Move() {
        _rigidBody.AddForce((_startClick - _nowClick) * speed);

        _bClicked = false;
	}
	#endregion

	#region event handlers
	#endregion
}
