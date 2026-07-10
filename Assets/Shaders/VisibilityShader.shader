Shader "Custom/VisibilityShader"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _VisionRadius ("Vision Radius", Float) = 5.0
        _FadeEdge ("Fade Edge", Float) = 1.0
        _IsVisible ("Is Visible", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 positionWS : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float _VisionRadius;
                float _FadeEdge;
                float _IsVisible;
            CBUFFER_END

            float4 _GlobalPlayerPos;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.positionCS = TransformWorldToHClip(output.positionWS);
                output.uv = input.uv;
                output.color = input.color * _Color;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv) * input.color;
                float dist = distance(input.positionWS.xy, _GlobalPlayerPos.xy);
                
                float distFade = 1.0 - saturate((dist - (_VisionRadius - _FadeEdge)) / _FadeEdge);
                
                col.a *= distFade;
                col.a *= _IsVisible;
                
                return col;
            }
            ENDHLSL
        }
    }
}