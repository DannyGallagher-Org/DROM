///
// TextFade - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;

public class TextFade : MonoBehaviour
{

	public bool PlayOnAwake = false;
	private bool _active = false;
	private TextMeshProUGUI _textMeshProUgui;
	public float Delay = 0f;
	public float Speed = 2f;

	public bool On;
	
	private void Awake()
	{
		if (PlayOnAwake)
		{
			hUtility.StaticCoroutinableObject.WaitForSecondsAsPromise(Delay)
				.Then(o => { _active = true; });
		}
		
		_textMeshProUgui = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if(_active)
			_textMeshProUgui.alpha = Mathf.Lerp(_textMeshProUgui.alpha, On ? 1f : 0f, Time.deltaTime*Speed);
	}

	public void FadeOn()
	{
		_active = true;
		On = true;
	}

	public void FadeOff()
	{
		_active = true;
		On = false;
	}
}
