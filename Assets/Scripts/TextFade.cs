///
// TextFade - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFade : MonoBehaviour {

	#region private variables
	#endregion

	#region public interface
	#endregion

	#region monobehaviour inherited
	void Update () {
		foreach (var t in GetComponentsInChildren<Text>()) {
			if (t.color.a > 0)
				t.color = new Color (1, 1, 1, t.color.a - Time.deltaTime);
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
