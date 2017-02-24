Shader "Custom/Rainbow" {
	Properties {
		_StartHue("Initial Hue", Float) = 0.0
		_WorldHeight("World Height", Float) = 5.0
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float3 worldPos;
		};

		float _StartHue;
		float _WorldHeight;
		half _Glossiness;
		half _Metallic;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float height = clamp(IN.worldPos.y + _WorldHeight, 0, 2 * _WorldHeight);
			float hue = height / (2 * _WorldHeight);
			
			float3 c = { hue, 1.0, 1.0 };

			//hsv conversion from http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
			float4 K = { 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 };
			float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			c = c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}

		ENDCG

	}
	FallBack "Diffuse"
}
