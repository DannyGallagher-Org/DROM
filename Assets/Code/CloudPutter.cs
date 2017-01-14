using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPutter : MonoBehaviour
{
    public GameObject[] clouds;

    void Awake()
    {
        StartCoroutine(CloudPutting());
    }

    public IEnumerator CloudPutting()
    {
        while (GameDefs.kbTrailer)
        {
            GameObject cloud = GameObject.Instantiate(clouds[Random.Range(0, clouds.Length)]);
            cloud.GetComponent<CloudController>().MoveOff(Random.Range(100f, 150f));
            Debug.Log("put");
            yield return new WaitForSeconds(15f);
        }
    }
}
