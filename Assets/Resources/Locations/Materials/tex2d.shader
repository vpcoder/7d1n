Shader "7d1n/Build/Tex2D"
{

	Properties
	{
		_Color("Color", Color) = (0,0,0,1)
		_MainTex("Texture", 2D) = "white" {}
		_Pos("Position", Float) = (0,0,0,0)
		_Zoom("Zoom", Float) = (0.2,0.2,1,1)
	}

	SubShader
	{

			Tags
			{ 
				"Queue"="Transparent"
				"IgnoreProjector"="True"
				"RenderType"="Transparent"
			}

			ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Less
			Cull Off

		Pass
		{

			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 pos: POSITION;
				};

				float4 _Zoom;
				sampler2D _MainTex;
				float4 _Color;

				v2f vert (
					float4 vertex : POSITION,
					float2 uv : TEXCOORD0
                )
				{
					v2f o;
					o.pos = UnityObjectToClipPos(vertex);
					o.uv = vertex;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float2 pos = i.uv * _Zoom;
					pos -= floor(pos);
					return tex2D(_MainTex, pos) * _Color;
				}

			ENDCG

		}

	}

}
