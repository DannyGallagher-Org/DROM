using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AudioManagerClass : MonoBehaviour 
{
	
	#region Vars
	
	// Fad enum
	private enum Fade {In, Out};
	
	// Soundtrack componant link
	private GameObject SoundtrackSourceObj;
	
	// voluem for toggling music
	private float _currentAudioVolume = 1f;
	private float _volumeOverride = 0.0001f;
	
	// Soundtrack queue
	private List<AudioClip> _soundtrackQueue = new List<AudioClip>();
	private bool _bSoundTrackQueued = false;
	private bool _bSoundtrackPaused = false;
	
	private float _pitchOverride;
	private string[] _ignorAudioArray;
	
	// Ipod flag
	private bool isIpodMusicPlaying = false;
	public bool IsIpodMusicPlaying
	{
		get { return isIpodMusicPlaying; }
		set { isIpodMusicPlaying = value; }
	}
	
	// Soundtrack flag
	private bool _isSoundTrack = true;
	public bool IsSoundTrack
	{
		get { return _isSoundTrack; }
		set { 
			_isSoundTrack = value;

		}
	}
	
	// SoundFX flag
	private bool isSoundFX = true;
	public bool IsSoundFX
	{
		get { return isSoundFX; }
		set { 
			isSoundFX = value;

		}
	}
	
	private string tagST = "AudioST";
	private string tagSFX = "AudioSFX";	
	
	#endregion
	
	#region Initialization
	
	// Use this for pre-initialization
	void Awake()
	{
		// Set audio flags from saved data

		
		_pitchOverride = 1;
	}
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	#endregion
	
	#region Update
	
	void Update()
	{
		if(SoundtrackSourceObj == null)
			return;
		
		if (_bSoundTrackQueued && !_bSoundtrackPaused)
		{
			if (SoundtrackSourceObj.GetComponent<AudioSource>().isPlaying)
				return;
			else
			{
				bool loop = false;
				
				if (_soundtrackQueue.Count > 1)
					loop = false;
				else
					loop = true;
				
				PlaySoundtrack(_soundtrackQueue[0], SoundtrackSourceObj.GetComponent<AudioSource>().volume, 0f, loop);
				_soundtrackQueue.Remove(_soundtrackQueue[0]);
				
				if (_soundtrackQueue.Count < 1)
					_bSoundTrackQueued = false;
			}
		}
	}
	
	#endregion
	
	#region Public functions
	
	public void ToggleMusic(bool toggle)
	{
		GameObject currentSoundtrackObj = GameObject.FindWithTag(tagST);
		
		if (currentSoundtrackObj != null)
		{
			if (toggle)
			{
				_volumeOverride = 0.0001f;
				currentSoundtrackObj.GetComponent<AudioSource>().volume = 0.0001f;
			}
			else
			{
				_volumeOverride = 1f;
				currentSoundtrackObj.GetComponent<AudioSource>().volume = _currentAudioVolume;
			}
		}
	}
	
	#endregion
	
	#region SoundTrack
	
	// Queues an clip for soundtrack
	public void QueueSoundtrack(AudioClip clip)
	{
		_soundtrackQueue.Add(clip);
		_bSoundTrackQueued = true;
	}
	
	// Plays a supplyed clip as the soundtrack
	public void PlaySoundtrack(AudioClip clip, float volume = 1.0f,  float fadeInDuration = 1.0f, bool loop = true)
	{
		if(IsSoundTrack)
			_volumeOverride = 1f;
		else
			_volumeOverride = 0.001f;
		
		GameObject currentSoundtrackObj = GameObject.FindWithTag(tagST);
		
		_currentAudioVolume = volume;
		
		if (currentSoundtrackObj != null)
		{
			if (currentSoundtrackObj.name == (tagST + ": " + clip.name))
				return;
			else
				StopSoundtrack(1);
		}
		
		// Create an empty game object
		GameObject AudioObj = new GameObject(tagST + ": " + clip.name);
		AudioObj.transform.position = Vector3.zero;
		AudioObj.tag = tagST;
		
		DontDestroyOnLoad(AudioObj);
		
		// Create the source
		AudioSource source = AudioObj.AddComponent<AudioSource>();
		source.clip = clip;
		source.loop = loop;
		source.volume = volume * _volumeOverride;
		source.pitch = 1f;
		source.playOnAwake = false;
		
		if (!isIpodMusicPlaying)
			source.Play();
		
		SoundtrackSourceObj = AudioObj;
		
		//		StartCoroutine(FadeAudio(SoundtrackSourceObj, fadeInDuration, Fade.In));
	}
	
	// Stops the current soundtrack
	public void StopSoundtrack(float fadeOutVal, bool clearQueue = false) {
		if (SoundtrackSourceObj == null) return;
		
		if (clearQueue) _soundtrackQueue.Clear();
		
		SoundtrackSourceObj.GetComponent<AudioSource>().Stop();
		Destroy(SoundtrackSourceObj);
		
		//		if (fadeOutVal == 0)
		//		{
		//		}
		//		else
		//			StartCoroutine(FadeAudio(SoundtrackSourceObj, fadeOutVal, Fade.Out));
	}
	
	// Pauses the current soundtrack
	public void PauseSoundtrack() {
		
		if (isIpodMusicPlaying || !_isSoundTrack) return;
		
		_bSoundtrackPaused = true;
		
		if (SoundtrackSourceObj != null)
			if (SoundtrackSourceObj.GetComponent<AudioSource>().isPlaying)
				SoundtrackSourceObj.GetComponent<AudioSource>().Pause();
	}
	
	// UnPauses the current soundtrack
	public void UnPauseSoundtrack() {
		
		if (isIpodMusicPlaying || !_isSoundTrack) return;
		
		if (SoundtrackSourceObj != null)
			if (!SoundtrackSourceObj.GetComponent<AudioSource>().isPlaying)
				SoundtrackSourceObj.GetComponent<AudioSource>().Play();
		
		_bSoundtrackPaused = false;
		
	}
	
	#endregion
	
	#region SFX
	
	public AudioSource PlaySFX(AudioClip clip)
	{
		return PlaySFX(clip, false, 1f, 1f, 0f, null);
	}
	
	public AudioSource PlaySFX(AudioClip clip, bool loop)
	{
		return PlaySFX(clip, loop, 1f, 1f, 0f, null);
	}
	
	public AudioSource PlaySFX(AudioClip clip, bool loop, float volume)
	{
		return PlaySFX(clip, loop, volume, 1f, 0f, null);
	}
	
	public AudioSource PlaySFX(AudioClip clip, bool loop, float volume, float pitch)
	{
		return PlaySFX(clip, loop, volume, pitch, 0f, null);
	}
	
	public AudioSource PlaySFX(AudioClip clip, bool loop, float volume, float pitch, float fadeInDuration)
	{
		return PlaySFX(clip, loop, volume, pitch, fadeInDuration, null);
	}
	
	// Plays a sound at the given point in space by creating an empty game object with an AudioSource
	// in that place and destroys it after it finished playing.
	public AudioSource PlaySFX(AudioClip clip, bool loop, float volume, float pitch, float fadeInDuration, Transform trans)
	{
		// Is the sound effect flag is of, return without playing
		if (!isSoundFX) return null;
		
		// Create an empty game object
		GameObject AudioObj = new GameObject(tagSFX + ": " + clip.name);
		AudioObj.transform.position = Vector3.zero;
		AudioObj.tag = tagSFX;
		
		// If the transform exsists, parent it
		if (trans != null)
			AudioObj.transform.parent = trans;
		
		// Create the source
		AudioSource source = AudioObj.AddComponent<AudioSource>();
		source.clip = clip;
		source.loop = loop;
		source.volume = volume;
		source.pitch = pitch;
		source.playOnAwake = false;
		
		// Override the pitch
		if(_pitchOverride != 1 && pitch == 1)
		{
			// Check if there is an ignor list to check
			if(_ignorAudioArray != null)	
			{
				// Check if the audio clip should be ignored
				if(!_ignorAudioArray.Any(source.clip.name.Contains))
				{
					source.pitch = _pitchOverride;
				}
			}
			else
			{
				source.pitch = _pitchOverride;
			}
		}
		
		source.Play();
		
		if (fadeInDuration > 0)
			StartCoroutine(FadeAudio(AudioObj, fadeInDuration, Fade.In));
		
		if (!loop)
			Destroy(AudioObj, clip.length);
		
		//Debug.Log("Playing SFX: " + clip.ToString());
		
		return source;
	}
	
	// Stops a SFX from playing by deleting the object or it was tied to or removing the audio source component.
	public void StopSFX(AudioSource source, float fadeTime = 0f, bool deleteObject = true) {
		
		if (source == null) return;
		
		GameObject AudioObj = source.gameObject;
		
		if (AudioObj == null) return;
		
		if (fadeTime <= 0) {
			if (deleteObject)
				Destroy(AudioObj);
			else
				Destroy (AudioObj.GetComponent<AudioSource>());
		}
		else {
			StartCoroutine(FadeAudio(AudioObj, fadeTime, Fade.Out, deleteObject));
		}
	}
	
	// Stops all SFX from playing by deleting there objects
	public void StopAllSFX() {
		
		foreach(GameObject AudioObj in GameObject.FindGameObjectsWithTag(tagSFX))
		{
			Destroy(AudioObj);
		}
	}
	
	#endregion
	
	#region Universal Audio Functions
	
	// Stops all audio
	public void StopAllAudio() {
		
		StopAllSFX();
		StopSoundtrack(0);
	}
	
	// Pause all Audio
	public void PauseAllAudio() {
		
		PauseSoundtrack();
		
		foreach(GameObject AudioObj in GameObject.FindGameObjectsWithTag(tagSFX))
		{
			AudioObj.GetComponent<AudioSource>().Pause();
		}
	}
	
	// UnPause all Audio
	public void UnPauseAllAudio() {
		
		UnPauseSoundtrack();
		
		foreach(GameObject AudioObj in GameObject.FindGameObjectsWithTag(tagSFX))
		{
			AudioObj.GetComponent<AudioSource>().Play();
		}
	}
	
	// Overrides the pitch for a set amount of time
	public void PitchAllAudio(float newPitch) {
		
		_pitchOverride = newPitch;
		
		// Get all the audio source in the scene
		AudioSource[] audioSourceArray = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		
		// Run through each source
		foreach(AudioSource audioSource in audioSourceArray)
		{
			// check if we have a ignor list to check
			if(_ignorAudioArray != null)	
			{
				// check if the ignor list contains this audio clip
				if(_ignorAudioArray.Any(audioSource.clip.name.Contains))
				{
					break;
				}
			}
			
			audioSource.pitch = _pitchOverride;
		}
	}
	
	//
	//	Sets the audio to ignor pitch
	//
	public void SetAudioStringsToIgnorPitch(string[] ignorAudioArray)
	{
		_ignorAudioArray = ignorAudioArray;
	}
	
	// Fades out a supplyed audio source
	IEnumerator FadeAudio (GameObject sourceObj, float timer, Fade fadeType, bool deleteObject = false) {
		
		AudioSource source = sourceObj.GetComponent<AudioSource>();
		
		float start = (fadeType == Fade.In) ? 0.0f : source.volume;
		float end = (fadeType == Fade.In) ? source.volume : 0.0f;
		float i = 0.0f;
		float step = ((fadeType == Fade.In) ? end : start)/timer;
		
		while (i <= 1.0f) {
			
			if (source == null) {
				
				break;
			}
			
			i += step * Time.deltaTime;
			source.volume = Mathf.Lerp(start, end, i);
			
			yield return new WaitForSeconds(step * Time.deltaTime);
		}
		
		if (source != null) {
			if (fadeType == Fade.Out || deleteObject) {
				
				if (source.tag == tagST)
					Destroy(source.gameObject);
				else if (source.tag == tagSFX)
					StopSFX(source, 0f, deleteObject);
			}
		}
	}
	
	#endregion
	
	#region Callbacks from Ipod
	
	// Callback if ipod music is playing
	void IpodMusicHasBeenSelected (string message) {
		
		isIpodMusicPlaying = true;

		Debug.Log (message);
	}
	
	// callback is ipod music selection has been cancelled
	void IpodMusicSelectionCanceled (string message) {
		
		UnPauseSoundtrack();

		Debug.Log (message);
	}
	
	#endregion
}
