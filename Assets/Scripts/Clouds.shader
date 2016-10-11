Shader "Drom/Goop"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" { }

	_Color("Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor("OutlineColor", Color) = (0,0,0,1)
		_HighlightColor("HighlightColor", Color) = (1,1,1,1)

		_UseSpriteColor("Use Sprite Color", Range(0, 1)) = 0.0

		_SolidThreshold("SolidThreshold", Range(0,1)) = 0.3
		_OutlineThreshold("OutlineThreshold", Range(0,1)) = 0.2

		_HighlightX("HighlightX", Range(-1,1)) = 0.5
		_HighlightY("HighlightY", Range(-1,1)) = 0.5

	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" }

		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag	
#include "UnityCG.cginc"	

		sampler2D _MainTex;

	half4 _MainTex_ST;

	half4 _Color;
	half4 _OutlineColor;
	half4 _HighlightColor;

	half _UseSpriteColor;

	half _SolidThreshold;
	half _OutlineThreshold;

	half _HighlightX;
	half _HighlightY;

	struct appdata_t
	{
		half4 vertex : POSITION;
		half2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		half4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
	};

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	half4 frag(v2f input) : COLOR
	{
		half4 outColor = tex2D(_MainTex, input.texcoord);

		half4 compareColor = tex2D(_MainTex, input.texcoord + half2(_HighlightX, _HighlightY) * 0.01);

		half isSolid = step(_SolidThreshold, outColor.a);
		half isOutline = step(_OutlineThreshold, outColor.a);

		half highlight = (outColor.a - compareColor.a) * 4;

		highlight = step(0.5, highlight);



		half4 goopColor = lerp(_Color, half4(compareColor.r, compareColor.g, compareColor.b, _Color.a), _UseSpriteColor);

		outColor = lerp(goopColor, _HighlightColor, highlight);
		outColor = lerp(_OutlineColor, outColor, isSolid);
		outColor = lerp(half4(0,0,0,0), outColor, isOutline);



		return outColor;
	}

		ENDCG

	}
	}
		Fallback "VertexLit"
}