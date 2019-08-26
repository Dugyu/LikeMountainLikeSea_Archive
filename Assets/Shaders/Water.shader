Shader "Dugy/Unlit/Water"
{
	Properties
	{
		_Color("Tint", Color) = (1, 1, 1, .5)
		_MainTex("Main Texture", 2D) = "white" {}
		_ColorTex("Color Texture", 2D) = "white" {}
		_TextureDistort("Texture Wobble", range(0,1)) = 0.1
		_NoiseTex("Extra Wave Noise", 2D) = "white" {}
		_Speed("Wave Speed", Range(0,1)) = 0.5
		_Amount("Wave Amount", Range(0,1)) = 0.6
		_Scale("Scale", Range(0,1)) = 0.5
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent"  "Queue" = "Transparent" "IgnoreProjector" = "True"}
			LOD 100
			Blend OneMinusDstColor One
			Cull Off

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float2 uv : TEXCOORD3;
					UNITY_FOG_COORDS(1)
					float4 worldPos : TEXCOORD4;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				float _TextureDistort;
				float4 _Color;
				sampler2D _MainTex, _NoiseTex,_ColorTex; 
				float4 _MainTex_ST, _ColorTex_ST;
				float _Speed, _Amount, _Scale;

				v2f vert(appdata v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);  
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed distortx = tex2D(_NoiseTex, (i.worldPos.xz * _Scale) + (_Time.x * 2)).r;// distortion alpha
					half4 col = tex2D(_MainTex, (i.worldPos.xz * _Scale) - (distortx * _TextureDistort));// texture times tint;        
					col *= _Color;
					col = saturate(col) * col.a;
					clip(col.g > 0.5 ? 1 : -1);
					col = col * tex2D(_ColorTex, i.uv);
					return col;
				}
				ENDCG
			}
		}
}
