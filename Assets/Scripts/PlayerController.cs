///
// PlayerController - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    #region private variables
    public Camera cloudCam;
    private bool _bDragging;
    private List<Transform> _cloudsTouched = new List<Transform>();

    private Vector3 _mouseClickPos;
    #endregion

    #region public interface
    #endregion

    #region monobehaviour inherited
    void Awake()
    {

    }
    #endregion

    #region public methods
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !_bDragging)
        {
            _bDragging = true;

            Ray ray = cloudCam.ScreenPointToRay(Input.mousePosition);

            _mouseClickPos = Input.mousePosition;

            RaycastHit[] hits = Physics.SphereCastAll(ray.origin, 0.25f, ray.direction, Mathf.Infinity, ~LayerMask.NameToLayer("Cloud"));

            for (int i = hits.Length - 1; i > -1; i--)
            {
                if (hits[i].transform.GetComponent<CloudBit>())
                {
                    _cloudsTouched.Add(hits[i].transform);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && _bDragging)
        {
            _cloudsTouched.Clear();
            _bDragging = false;
        }

        if (_bDragging && Input.GetKey(KeyCode.Mouse0) && Vector2.Distance(_mouseClickPos, Input.mousePosition) > 1f)
        {
            foreach (var c in _cloudsTouched)
            {
                if(c)
                    c.GetComponent<CloudBit>().Move((Input.mousePosition - _mouseClickPos).normalized);
            }

            _cloudsTouched.Clear();
            _bDragging = false;
        }
    }
    #endregion

    #region private methods
    #endregion

    #region event handlers
    #endregion
}
