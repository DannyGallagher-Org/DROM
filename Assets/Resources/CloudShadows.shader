Shader "CloudShadows"
{
		Properties
		{
			_MainTex("Texture", 2D) = "white" { }

			_SolidThreshold("SolidThreshold", Range(0,1)) = 0.7
			_OutlineThreshold("OutlineThreshold", Range(0,1)) = 0.5
			_ShadowThreshold("ShadowThreshold", Range(0,1)) = 0.2

			_SolidColor("SolidColor", Color) = (1,1,1,0.8)
			_OutlineColor("OutlineColor", Color) = (1,1,1,0.4)
			_ShadowColor("ShadowColor", Color) = (0.7,0.4,0.8,0.8)
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

				half _SolidThreshold;
				half _OutlineThreshold;
				half _ShadowThreshold;

				half4 _OutlineColor;
				half4 _SolidColor;
				half4 _ShadowColor;

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

					half4 compareColor = tex2D(_MainTex, input.texcoord + half2(_ShadowThreshold, _ShadowThreshold*5) * 0.01);

					//half isSolid = step(_SolidThreshold, outColor.a);
					//half isOutline = step(_OutlineThreshold, outColor.a);

					half shadow = (outColor.a - compareColor.a) * 4;

					shadow = step(0.5, shadow);

					outColor = lerp(outColor, outColor*_ShadowColor, shadow);
					//outColor = lerp(_OutlineColor, outColor, isSolid);
					//outColor = lerp(half4(0,0,0,0), outColor, isOutline);

					return outColor;
				}

				ENDCG

			}
		}
		Fallback "VertexLit"
}