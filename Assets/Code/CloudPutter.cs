using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPutter : MonoBehaviour
{
    public GameObject[] clouds;
    private int _cloud;

    void Awake()
    {
        StartCoroutine(CloudPutting());
    }

    public IEnumerator CloudPutting()
    {
        while (GameDefs.kbTrailer)
        {
            GameObject cloud = GameObject.Instantiate(clouds[_cloud]);
            _cloud = _cloud >= clouds.Length - 1 ? 0 : _cloud + 1;
            cloud.GetComponent<CloudController>().MoveOff(Random.Range(100f, 150f));
            Debug.Log("put");
            yield return new WaitForSeconds(15f);
        }
    }
}
