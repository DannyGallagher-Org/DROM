///
// Thoughts - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using System.Collections;

public class Thoughts : MonoBehaviour {

	#region private variables
	private int shown = 0;
	#endregion

	#region public interface
	public GameObject[] Bubble;
	public GameObject Shape;
	#endregion

	#region monobehaviour inherited

	#endregion

	#region public methods
	public void Show() {
		InvokeRepeating ("ShowBubble", GameDefs.kCloudPuffDelayTime, GameDefs.kCloudPuffTime);
	}

	public void Hide() {
		InvokeRepeating ("HideBubble", 0f, GameDefs.kCloudPuffTime);
	}

	public void ShowBubble() {
		if (shown > 3) {
			CancelInvoke ("ShowBubble");
			Invoke ("Hide", GameDefs.kCloudPuffStayCompleteTime);
			shown = 3;
			return;
		}

		Bubble [shown].SetActive (true);
		shown++;
	}

	public void HideBubble() {
		if (shown < 1) {
			CancelInvoke ("HideBubble");
			Bubble [shown].SetActive (false);
			Shape.SetActive (true);
			shown = 0;
			return;
		}

		Bubble [shown].SetActive (false);
		shown--;
	}
	#endregion
}
