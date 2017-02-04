using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfternSeconds : MonoBehaviour 
{
	[SerializeField] private float _waitTime = 10.0f;

	private void Start()
	{
		StartCoroutine(KillAfter());
	}

	private IEnumerator KillAfter()
	{
		yield return new WaitForSeconds(_waitTime);
		Application.Quit();
	}
}