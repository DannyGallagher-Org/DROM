using UnityEngine;

namespace Code
{
	public class GameManager : MonoBehaviour {
    
		//managers
		public static AudioManager audioManager; 

		// Use this for initialization
		void Awake () {
			Debug.Log("HEY");
			audioManager = gameObject.AddComponent<AudioManager> ();
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
