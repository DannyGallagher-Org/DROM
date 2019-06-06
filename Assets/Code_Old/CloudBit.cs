///
// CloudBit - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class CloudBit : MonoBehaviour
{

    #region private variables
    #endregion

    #region public interface
    #endregion

    #region monobehaviour inherited
    void Awake()
    {
        var move = Vector3.zero;
        move.z += Random.Range(-5f, 5f);
        transform.localPosition += move;
    }
    #endregion

    #region public methods
    public void Move(Vector3 direction)
    {
        
    }
    #endregion

    #region private methods
    #endregion

    #region event handlers
    #endregion
}
