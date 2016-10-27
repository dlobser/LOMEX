﻿Shader "Unlit/City"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color",Color)= (1,1,1,1)
		_LightColorR("LightColorR",Color)=(1,1,1,1)
		_LightColorG("LightColorG",Color)=(1,1,1,1)
		_LightColorB("LightColorB",Color)=(1,1,1,1)
		_SecondTex("Second Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }//"Queue"="Transparent" }
//		Blend One One
//		Cull Off
//		ZWrite Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD2;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 color : COLOR;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SecondTex;
			float4 _SecondTex_ST;
			float4 _Color;
			float4 _LightColorR;
			float4 _LightColorG;
			float4 _LightColorB;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				fixed4 colR = _LightColorR*i.color.r;
				fixed4 colG = _LightColorG*i.color.g;
				fixed4 colB = _LightColorB*i.color.b;
				return colR+colG+colB+col*_Color;
			}
			ENDCG
		}
	}
}
