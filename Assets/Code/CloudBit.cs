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
    public bool _bMove = false;
    private float _moveTime = 0;

    private float dampener = 30f;

    private Vector3 force = Vector3.zero;
    #endregion

    #region public interface
    #endregion

    #region monobehaviour inherited
    void Awake()
    {

    }

    void Update()
    {
        transform.Translate(force, Space.Self);

        if (force.magnitude > 0)
            force -= (force * Time.deltaTime)*2.5f;
    }
    #endregion

    #region public methods
    public void Move(Vector3 direction)
    {
        force += direction;
        force.z = 0;
        force /= dampener;
    }
    #endregion

    #region private methods
    #endregion

    #region event handlers
    #endregion
}
