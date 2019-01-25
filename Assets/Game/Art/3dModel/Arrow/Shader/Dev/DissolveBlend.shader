Shader "Custom/DissolveBlend" {
	Properties {
	  _TintColor ("Tint Color", Color) = (1,1,1,1)
      _MainTex ("Main_Tex", 2D) = "white" {}
	  _BurnAmount ("Burn Amount", Range(0.0, 1.0)) = 0.0
	  _LineWidth("Burn Line Width", Range(0.0, 0.2)) = 0.1
	  _BurnFirstColor("Burn First Color", Color) = (0.58, 0.21, 0, 1)
	  _BurnSecondColor("Burn Second Color", Color) = (1, 0.56, 0, 1)
	 //_FlyThreshold("FlyThreshold", Range(0,1)) = 0.6
	 //_FlyFactor("FlyFactor", Range(0,2)) = 1.29
	  _BurnMap("Burn Map", 2D) = "white"{}
	}
	SubShader {
	   Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
		LOD 200
		Pass {
			Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
			Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _TintColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _BurnAmount;
			fixed _LineWidth;
			//fixed _FlyThreshold;
			//fixed _FlyFactor;
			fixed4 _BurnFirstColor;
			fixed4 _BurnSecondColor;
			sampler2D _BurnMap;
			float4 _BurnMap_ST;

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 vertexColor : COLOR;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				float2 uv_burnmap : TEXCOORD9;
				float4 vertexColor : COLOR;
			};


			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv_burnmap = TRANSFORM_TEX(v.texcoord, _BurnMap);
				o.vertexColor = v.vertexColor;
				/*
				fixed _burn = _BurnAmount * v.vertexColor.a;
				if(_burn >=_FlyThreshold)
				{
					fixed3 up = float3(0,-1,0);
					fixed3 obj_up = mul((float3x3)unity_WorldToObject, up);
					v.vertex.xyz -= obj_up * saturate( _BurnAmount - _FlyThreshold ) * _FlyFactor; 
				}
				*/
				return o;
			}

			fixed4 GetBurnColor(in v2f i, fixed4 c)
			{
				if (c.a > 0.001) {
					fixed3 burn = tex2D(_BurnMap, i.uv_burnmap).rgb;			
					fixed _amount = i.vertexColor.a *  _BurnAmount;

					clip(burn.r - _amount);
					fixed t = 1 - smoothstep(0.0, _LineWidth, burn.r - _amount);
					fixed3 burnColor = lerp(_BurnFirstColor, _BurnSecondColor, t);
					burnColor = pow(burnColor, 5);
					fixed4 burn_c = fixed4(burnColor, 1);
					fixed4 newColor = lerp(c, burn_c, t * step(0.0001, _amount));
					//newColor.a *= i.vertexColor.a;
					return newColor;
				}
				else
				{
					return c;
				}

				//return fixed4(0, 0, 0, 1);
				//return c;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				col = GetBurnColor(i, col);
				col.rgb = col.rgb * i.vertexColor.rgb;
				col = col * _TintColor;

				return col;
			}



			ENDCG
		}
	}
	FallBack "Diffuse"
}
