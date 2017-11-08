// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ContrastEnhancement" {
	Properties {
		_Percent ("Percent", Float) = 0.5
		_Cutoff ("Cutoff", Float) = 0.5
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

			float _Percent;
			float _Cutoff;
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

			float4 frag(vertexOut v) : COLOR // fragment shader
			{
				fixed4 color = tex2D(_MainTex, v.uv);
				float3 adjustedColor;
				float rgbAverage = (color[0] + color[1] + color[2]) / 3;
				if (rgbAverage > _Cutoff)
				{
					adjustedColor = float3(1.0 - color[0], 1.0 - color[1], 1.0 - color[2]);
					adjustedColor = adjustedColor * _Percent;
					adjustedColor.r = 1.0 - adjustedColor.r;
					adjustedColor.g = 1.0 - adjustedColor.g;
					adjustedColor.b = 1.0 - adjustedColor.b;
					return float4(adjustedColor, color.a);
				}
				else
				{
					adjustedColor = color.rgb * _Percent;
					return float4(adjustedColor, color.a);
				}
				// this fragment shader returns a nameless fragment
				// output parameter (with semantic COLOR) that is set to
				// opaque red (red = 1, green = 0, blue = 0, alpha = 1)
			}

			ENDCG // here ends the part in Cg 
		}
   }
}
