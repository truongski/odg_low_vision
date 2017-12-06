// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/EdgeDetectColor" {
	Properties{
		_EdgeDetectCutoff("Edge Detect Cutoff", Float) = 1.0
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader 
	{ // Unity chooses the subshader that fits the GPU best
		Pass
			{ // some shaders require multiple passes
				CGPROGRAM // here begins the part in Unity's Cg

#pragma vertex vert 
						  // this specifies the vert function as the vertex shader 
#pragma fragment frag
						  // this specifies the frag function as the fragment shader
#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct vertexIn
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertexOut
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			vertexOut vert(vertexIn v)
			{
				vertexOut o;
				o.uv = v.uv;
				o.pos = UnityObjectToClipPos(v.pos);
				return o;
				// this line transforms the vertex input parameter 
				// vertexPos with the built-in matrix UNITY_MATRIX_MVP
				// and returns it as a nameless vertex output parameter 
			}

			float _EdgeDetectCutoff;

			float4 frag(vertexOut v) : COLOR // fragment shader
			{
				float w = 1.0 / _ScreenParams.x;
				float h = 1.0 / _ScreenParams.y;

				float4 tex = tex2D(_MainTex, v.uv);
				float4 n[9];
				n[0] = tex2D(_MainTex, v.uv + float2(-w, -h));
				n[1] = tex2D(_MainTex, v.uv + float2(0.0, -h));
				n[2] = tex2D(_MainTex, v.uv + float2(w, -h));
				n[3] = tex2D(_MainTex, v.uv + float2(-w, 0.0));
				n[4] = tex2D(_MainTex, v.uv + float2(0.0, 0.0));
				n[5] = tex2D(_MainTex, v.uv + float2(w, 0.0));
				n[6] = tex2D(_MainTex, v.uv + float2(-w, h));
				n[7] = tex2D(_MainTex, v.uv + float2(0.0, h));
				n[8] = tex2D(_MainTex, v.uv + float2(w, h));

				float4 sobelEdgeH = n[2] + (2.0 * n[5]) + n[8] - (n[0] + (2.0 * n[3]) + n[6]);
				float4 sobelEdgeV = n[0] + (2.0 * n[1]) + n[2] - (n[6] + (2.0 * n[7]) + n[8]);
				float4 sobel = sqrt((sobelEdgeH * sobelEdgeH) + (sobelEdgeV * sobelEdgeV));

				float4 color = float4(1.0 - sobel.rgb, 1.0);
				if (sobel.r + sobel.g + sobel.b / 3 < _EdgeDetectCutoff)
				{
					color = tex;
				}
				return color;
			}

			ENDCG // here ends the part in Cg 
		}
   }
}
