///
// CloudBit - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class CloudBit : MonoBehaviour
{

    #region private variables
    private Vector3 _targetVec;
    private bool _bMove;

    private float dampener = 50f;

    private bool _bBursting = false;

    private Vector3 _force = Vector3.zero;
    #endregion

    #region public interface
    #endregion

    #region monobehaviour inherited

    void Update()
    {
        if(_bBursting)
        {

        }
        else
        {
            transform.Translate(_force, Space.Self);

            if (_force.magnitude > 0)
                _force -= (_force * Time.deltaTime)*2.5f;
        }
    }
    #endregion

    #region public methods
    public void Move(Vector3 direction)
    {
        _force += direction;
        _force.z = 0;
        _force /= dampener;
		Debug.Log ("cloud bit move called");
		//GameManager.audioManager.PlayCloudBitSound ();
    }
    #endregion

    #region private methods
    #endregion

    #region event handlers
    #endregion
}
