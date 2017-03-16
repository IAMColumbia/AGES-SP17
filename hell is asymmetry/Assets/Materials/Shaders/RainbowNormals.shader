Shader "Custom/RainbowNormals" {
	Properties {
		_ColorX ("Color X", Color) = (1,0,0,1)
		_ColorY ("Color Y", Color) = (0,1,0,1)
		_ColorZ ("Color Z", Color) = (0,0,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
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

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldNormal;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _ColorX;
		fixed4 _ColorY;
		fixed4 _ColorZ;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = (0, 0, 0, 1);
			c = _ColorX * abs(IN.worldNormal.x) + _ColorY * abs(IN.worldNormal.y) + _ColorZ * abs(IN.worldNormal.z);
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
