Shader "Custom/HeightMap"
{
	Properties
	{

	}
	SubShader{
		Tags
{
	"Queue" = "Transparent"
	"RenderType" = "Transparent"
}
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _DepthTexture;
				struct appdata 
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};
				struct v2f 
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				v2f vert(appdata i)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(i.vertex);
					o.uv = i.uv;
					return o;
				}


				float4 frag(v2f i) : SV_Target
				{
					float4 c = tex2D(_DepthTexture,i.uv);
					return c;
				}
				ENDCG
			}
	}
}
