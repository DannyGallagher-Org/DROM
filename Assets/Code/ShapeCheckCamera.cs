///
// ShapeCheckCamera - By Daniel Gallagher
// Copyright Funny Looking Games 2016
///

using System;
using System.Collections;
using UnityEngine;

namespace Code
{
	public class ShapeCheckCamera : MonoBehaviour 
	{

		#region private variables
		private Camera _camera;

		private bool _bFirstCheck = true;
		private bool _bChecking;
		#endregion

		#region targets
		public float WholeScreenPixels;
	
		public float StartRatioRed;
		public float CurrentRatioRed;

		public float CurrentRatioBlue;
		public float CurrentRatioGreen;

		public float WinMargin;
		public bool Win;
		#endregion
	
		#region public interface
		public Texture2D Texture;
	
		public float Blue = 0;
		public float Red = 0;
		public float Green = 0;
		#endregion

		#region monobehaviour inherited
		private void Awake()
		{
			WholeScreenPixels = Screen.height*Screen.width;
		}

		void Start() 
		{
			_camera = GetComponent<Camera> ();

			_bChecking = true;
			StartCoroutine(CheckCoroutine());
		}
		#endregion

		#region private methods

		private IEnumerator CheckCoroutine()
		{
			while (_bChecking)
			{
				Destroy(Texture);
				var rt = new RenderTexture(Screen.width, Screen.height, 24);
				_camera.targetTexture = rt;

				Texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
				_camera.Render();
				RenderTexture.active = rt;
				Texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
				_camera.targetTexture = null;
				RenderTexture.active = null; // JC: added to avoid errors
				Destroy(rt);

				var pixels = Texture.GetPixels32();

				Green = Blue = Red = 0;

				foreach (var p in pixels)
				{
					if (p.b > 0.8f && ((p.r < 0.2f) && (p.g < 0.2f)))
						Blue++;

					if (p.r > 0.8f && ((p.b < 0.2f) && (p.g < 0.2f)))
						Red++;

					if (p.g > 0.8f && ((p.r < 0.2f) && (p.b < 0.2f)))
						Green++;
				}

				CurrentRatioBlue = Blue / WholeScreenPixels;
				CurrentRatioRed = Red / WholeScreenPixels;
				CurrentRatioGreen = Green / WholeScreenPixels;

				if (_bFirstCheck && Math.Abs(CurrentRatioRed) > 0.01f)
				{
					StartRatioRed = CurrentRatioRed;
					_bFirstCheck = false;
				}
				
				Win = Math.Abs(CurrentRatioRed - StartRatioRed) < WinMargin && CurrentRatioBlue < WinMargin;
				
				yield return new WaitForSeconds(0.33f);
			}

			#endregion

			#region event handlers

			#endregion
		}
	}
}
