using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CloudeShadowEffect : MonoBehaviour {

	private Material material;

    public Color solidColor;
    public float solidThreshhold = 0.3f;

    public Color outlineColor;
    public float outlineThreshhold = 0.1f;

    public Color shadowColor;
    public float shadowThreshhold = 0.1f;

    public float blur = 0.0005f;

    // Creates a private material used to the effect
    void Awake ()
	{
		material = new Material( Shader.Find("CloudShadows") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
        material.SetColor("_SolidColor", solidColor);
        material.SetFloat("_SolidThreshold", solidThreshhold);

        material.SetColor("_OutlineColor", outlineColor);
        material.SetFloat("_OutlineThreshold", outlineThreshhold);

        material.SetColor("_ShadowColor", shadowColor);
        material.SetFloat("_ShadowThreshold", shadowThreshhold);

        material.SetFloat("_BlurAmount", blur);

        Graphics.Blit (source, destination, material);
	}
}