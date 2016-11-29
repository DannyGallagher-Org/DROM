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

    private float speed = 20f;
    #endregion

    #region public interface
    #endregion

    #region monobehaviour inherited
    void Awake()
    {

    }

    void Update()
    {
        if (_bMove)
        {
            if (_moveTime < 0.5f)
            {
                _moveTime += Time.deltaTime;
                speed -= (Time.deltaTime * 5f);
                transform.Translate(_targetVec.normalized * speed * Time.deltaTime);
                //                Boss.audio.PlaySFX (Resources.Load ("sh") as AudioClip, false, 0.1f, Random.Range (0.8f, 1.2f), 0.01f);
            }
            else
            {
                speed = 20f;
                _bMove = false;
                _moveTime = 0;
            }
        }
    }
    #endregion

    #region public methods
    public void Move(Vector3 direction)
    {
        _targetVec = direction;
        _targetVec.z = 0;
        _bMove = true;
    }
    #endregion

    #region private methods
    #endregion

    #region event handlers
    #endregion
}
