﻿// Author : Maxime JUMELLE
// Project : More Post-Processing Effects Package
// If you have any suggestion or comment, you can write me at webmaster[at]hardgames3d.com

Shader "MorePPEffects/Wiggle" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
	float timer, speed;
	float amplitudeX, amplitudeY;
	float distortionX, distortionY;
	
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	float4 frag(v2f i) : SV_Target 
	{
		float2 texCoords = i.uv;
		texCoords.x += sin(timer * speed + texCoords.x * amplitudeX) * distortionX * 0.01f;
	    texCoords.y += cos(timer * speed + texCoords.y * amplitudeY) * distortionY * 0.01f;

	    return tex2D(_MainTex, texCoords);
	}

	ENDCG 
	
Subshader {
 Pass {
	  ZTest Always Cull Off ZWrite Off

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
  }
  
}

Fallback off
	
}
