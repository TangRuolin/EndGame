// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PHero/Particles/ParticleAdditiveMask2" {
Properties {
	_TintColor ("Tint Color", Color) = (1.0,1.0,1.0,1.0)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_MaskTex ("Mask Texture", 2D) = "white"{}
	_Rotation("Mask Texture Rotation", Range(0, 360)) = 0
	
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
}

Category {
	Tags {"IgnoreProjector"="True" "RenderType"="Transparent" "Queue"="Transparent" }
	Blend SrcAlpha One
	ColorMask RGBA
	Cull Off Lighting Off ZWrite Off Fog {Mode Off}
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles

			#include "UnityCG.cginc"

			fixed4 _TintColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			uniform float _Rotation;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord 	: TEXCOORD0;
				float2 maskTexcoord : TEXCOORD1;
				#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD2;
				#endif
			};
			

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				#endif
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				
				o.maskTexcoord = o.texcoord;
				if(_Rotation > 36)//hack
				{
					float2 tmp = o.maskTexcoord.xy;
					o.maskTexcoord.x = tmp.y;
					o.maskTexcoord.y = tmp.x;
				}
				
				return o;
			}

			sampler2D _CameraDepthTexture;
			float _InvFade;
			
			fixed4 frag (v2f i) : COLOR
			{
				half4 color = tex2D(_MainTex, i.texcoord) * i.color;
				color *= _TintColor;
				color.a = tex2D(_MaskTex, i.texcoord).a * i.color.a * _TintColor.a;
				return  color;
				
			}
			ENDCG 
		}
	}	
}
}
