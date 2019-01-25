// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ProjectionNormalAdd" {  
Properties {  
    _MainTex ("Base (RGB)", 2D) = "white" {}  
	_Color ("Tint", Color) = (1,1,1,1)
}  
SubShader {  
    Pass{  
        Tags {"RenderType"="Transparent"}  
        LOD 200  
        ZWrite off  
        //Blend SrcAlpha OneMinusSrcAlpha  
		Blend SrcAlpha One
        CGPROGRAM  
        #pragma vertex vert  
        #pragma fragment frag  
        #include "UnityCG.cginc"  
  
        sampler2D _MainTex;  
        float4x4 unity_Projector;  
		float4 _Color;

		struct a2v {
			float4 pos : POSITION;
			float4 uv : TEXCOORD0;
		};

        struct v2f  
        {  
            float4 pos:SV_POSITION;  
            float4 uv:TEXCOORD0;  
        };  
  
        v2f vert(a2v v)  
        {  
            v2f o;  
            o.pos = UnityObjectToClipPos(v.pos);  
            //将顶点变换到矩阵空间  
            o.uv = mul(unity_Projector,v.pos);  
            return o;  
        }  
  
        float4 frag(v2f o):COLOR  
        {  
            //对光环图片进行投影采样  
			//float4 c = tex2D(_MainTex,o.texc);
            float4 c = tex2Dproj(_MainTex,o.uv);  
            //限制投影方向  
            //c = c*step(0,o.uv.w);
			c = c * _Color;

            return c;  
        }  
  
        ENDCG  
        }  
    }  
    FallBack "Diffuse"  
}  