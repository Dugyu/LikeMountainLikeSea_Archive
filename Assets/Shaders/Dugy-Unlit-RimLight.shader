//Basic Unlit

Shader "Dugy/Unlit/RimLight" {
Properties {
    _MainTex ("MainTex", 2D) = "white" {}
	_rimColor ("RimColor", Color) = (1.0,0.5,0.25,0.5)
}

Subshader {
    Tags{"Queue" = "Transparent" "RenderType" = "Transparent"}
    LOD 100
	Blend SrcAlpha One
	ZWrite Off

	Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.5

			#include "UnityCG.cginc"

			struct vertData
			{
				float4 pos: POSITION;    // ** Model position, Model Space  
				float3 normal: NORMAL;   // surface normal vector, Model Space
				float2 uv: TEXCOORD0;	 // 0 - 1 UV coordinates
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv: TEXCOORD0;
				float3 obj_normal: TEXCOORD1;
				float3 world_normal: TEXCOORD2;
				float3 obj_pos: TEXCOORD3;
				float3 world_pos: TEXCOORD4;
				float4 view_pos: TEXCOORD5;     // ** View
				float4 sv_pos: SV_POSITION;     // ** Projection Clip  Final Output   Screen Space

				UNITY_VERTEX_OUTPUT_STEREO
			};



			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _rimColor;

			v2f vert(vertData v)
			{
				v2f o;
 				
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				
				o.obj_pos = v.pos;
				o.world_pos = mul(unity_ObjectToWorld, v.pos);
				o.obj_normal = v.normal;
				o.world_normal = mul(unity_ObjectToWorld, v.normal);
				o.sv_pos = UnityObjectToClipPos(v.pos);   // unity will transfer it to pixels in later pipeline
				o.view_pos = o.sv_pos;      // normalized viewport coordinates
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i): SV_TARGET
			{
				fixed4 col = tex2D(_MainTex, i.uv);
 				float3 toCam = normalize(WorldSpaceViewDir(float4(i.world_pos,1.0)));
				float3 fragNormal = normalize(i.world_normal);
				float rimLight = 1.0 - max(0.0, dot(fragNormal,toCam));
				rimLight = pow(rimLight, 2);
				//col.rgb = saturate(col.rgb + rimLight);
				return col*_rimColor;
			}


		ENDCG
	}
}

}