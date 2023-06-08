Shader "Custom/Invert_URP" 
{
    Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader 
    {
        Tags 
        { 
            "RenderPipeline" = "UniversalPipeline" 
            "Queue"="Transparent" 
        }
        LOD 200

        Pass
        {
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }

            Cull Off
            ZWrite Off
            ZTest Always

            Blend OneMinusDstColor Zero

            HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = float2(0,0);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDHLSL

        }
    }
    FallBack "Diffuse"
}
