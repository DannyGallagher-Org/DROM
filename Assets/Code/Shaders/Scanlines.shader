// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Scanlines"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Lighten("", Float) = 0
		_GapLighten("", Float) = 0

		_Contrast("Contrast", Float) = 0
		_Br("Brightness", Float) = 0
	}
	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off Fog { Mode off }


			CGPROGRAM
			

			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			#pragma target 3.0

			struct v2f
			{
				float4 pos		: POSITION;
				float2 uv		: TEXCOORD0;
				float4 scr_pos	: TEXCOORD1;
			};


			uniform sampler2D _MainTex;
			uniform float _Lighten;
			uniform float _GapLighten;

			uniform float _Contrast;
			uniform float _Br;

			v2f vert(appdata_img v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
				o.scr_pos = ComputeScreenPos(o.pos);

				return o;
			}

			half4 frag(v2f i): COLOR
			{
				float2 ps = i.scr_pos.xy * _ScreenParams.xy / i.scr_pos.w;
				half4 color = tex2D(_MainTex, i.uv);

				int pd = (int)(ps.y / 4) % 2;

				if (pd == 0)
				{
					float2 fuv = i.uv;
					fuv.x += 1.0 / (_ScreenParams.xy / i.scr_pos.w);
					color = tex2D(_MainTex, fuv);
				}
				
				int pl = (int)ps.x % 3;
				int ph = (int)ps.y % 4;

				float4 outcolor = float4(0, 0, 0, 1);
				float4 muls = float4(0, 0, 0, 0);

				color += (_Br / 255);
				color = color - _Contrast * (color - 1.0) * color *(color - 0.5);

				if (pl == 1)
				{
					outcolor.r = color.r;
					outcolor.g = color.g*_Lighten;
					outcolor.b = color.b*_Lighten;
				}
				else if (pl == 2)
				{
					outcolor.r = color.r*_Lighten;
					outcolor.g = color.g;
					outcolor.b = color.b*_Lighten;
				}
				else
				{
					outcolor.r = color.r*_Lighten;
					outcolor.g = color.g*_Lighten;
					outcolor.b = color.b;
				}

				if (ph == 0)
					outcolor = color*_GapLighten;

				return outcolor;
			}

			ENDCG
		}
	}
		FallBack "Diffuse"
}
