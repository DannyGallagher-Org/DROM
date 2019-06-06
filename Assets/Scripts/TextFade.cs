///
// TextFade - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextFade : MonoBehaviour {
	
	private TextMeshProUGUI _textMeshProUgui;
	public float Speed = 2f;

	public bool On;
	
	private void Awake()
	{
		_textMeshProUgui = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		_textMeshProUgui.alpha = Mathf.Lerp(_textMeshProUgui.alpha, On ? 1f : 0f, Time.deltaTime*Speed);
	}

	public void FadeOn()
	{
		On = true;
	}

	public void FadeOff()
	{
		On = false;
	}
}
