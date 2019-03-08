// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Visualizer"
{
	Properties
	{
		_Overlay("Overlay Texture", 2D) = "Clear" {}
	}

		SubShader
		{
			Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"

		}
			Cull Off
			CGPROGRAM
	#pragma surface surf Lambert vertex:myvert alpha
			struct Input
		{
			float2 uv_MainTex;
			float3 worldV;
			float3 col;
		};
		sampler2D _Overlay;
		float _xCenter;
		float _zCenter;
		float _xWidth;
		float _zWidth;

		void myvert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			// convert the vertex to world space: 
			data.worldV = mul(unity_ObjectToWorld, v.vertex.xyzw).xyz;

		}
		void surf(Input i, inout SurfaceOutput o)
		{
			float3 W = i.worldV;
			float botX = _xCenter - _xWidth / 2;
			float botZ = _zCenter - _zWidth / 2;
			float2 overlayUV = float2((W.x - botX) / _xWidth, (W.z - botZ) / _zWidth);
			float4 col = tex2D(_Overlay, overlayUV);
			o.Albedo = col.rgb;
			o.Alpha = col.a;

		}
		ENDCG
		}
}