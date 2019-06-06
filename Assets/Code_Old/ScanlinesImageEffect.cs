/*===============================================================
Product:    #PROJECTNAME#
Developer:  #DEVELOPERNAME#
Company:    #COMPANY#
Date:       #CREATIONDATE#
================================================================*/

using UnityEngine;

[ExecuteInEditMode]
public class ScanlinesImageEffect : MonoBehaviour {

	public Shader scanlineShader;
	private Material _material;

	[Range(0, 1)]
	public float lighten = 0.0f;
	[Range(0, 1)]
	public float gaplighten = 0.0f;

	[Range(-3, 20)]
	public float contrast = 0.0f;
	[Range(-200, 200)]
	public float brightness = 0.0f;

	protected Material material
	{
		get
		{
			if (_material == null)
			{
				_material = new Material(scanlineShader);
				_material.hideFlags = HideFlags.HideAndDontSave;
			}

			return _material;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Lighten", lighten);
		material.SetFloat("_GapLighten", gaplighten);
		material.SetFloat("_Contrast", contrast);
		material.SetFloat("_Br", brightness);
		Graphics.Blit(source, destination, material);
	}
}