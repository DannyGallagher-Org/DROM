///
// TextFade - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFade : MonoBehaviour {

	#region private variables
	public Color c;
	#endregion

	#region public interface
	public float target = 0f;
	#endregion

	#region monobehaviour inherited
	void Update () {
		foreach (var t in GetComponentsInChildren<Text>()) {
			if (!Mathf.Approximately (t.color.a, target)) {
				float a = Mathf.Lerp (t.color.a, target, Time.deltaTime);
				t.color = new Color (c.r, c.g, c.b, a);
			}
		}
	}
	#endregion

	#region public methods
	#endregion

	#region private methods
	#endregion

	#region event handlers
	#endregion
}
