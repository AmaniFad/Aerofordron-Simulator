// URP version of the grass shader
Shader "Bytesized/URP_Grass"
{
    Properties
    {
        _TopColor("Top Color", Color) = (0.57, 0.84, 0.32, 1.0)
        _BottomColor("Bottom Color", Color) = (0.0625, 0.375, 0.07, 1.0)
        _TranslucentGain("Translucent Gain", Range(0,1)) = 0.5
        _WindStrength("Wind Strength", Range(0.0001, 1)) = 0.3
        _ViewLOD("View Radius", Float) = 48
        _MaxStages("Max Stages", Range(2, 64)) = 7
        _BaseStages("Base Stages", Range(-64, 64)) = -0.5
        _BladeWidth("Blade Width", Range(0, 0.4)) = 0.05
        _BladeWidthRandom("Blade Width Random", Range(0, 0.4)) = 0.02
        _BladeHeight("Blade Height", Float) = 0.5
        _BladeHeightRandom("Blade Height Random", Float) = 0.3
        _BladeForward("Blade Stiffness Amount", Range(0, 1)) = 0.38
        _BladeCurve("Blade Curvature Amount", Range(1, 4)) = 2
        _BendRotationRandom("Bend Rotation Random", Range(0, 1)) = 0.2
    }

        SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "Queue" = "Geometry" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
            };

            CBUFFER_START(UnityPerMaterial)
            float4 _TopColor;
            float4 _BottomColor;
            float _TranslucentGain;
            float _WindStrength;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                Light mainLight = GetMainLight();
                float3 normal = normalize(IN.normalWS);
                float NdotL = saturate(dot(normal, mainLight.direction) + _TranslucentGain);
                float3 ambient = SampleSH(normal);
                float4 lightColor = NdotL * mainLight.color + float4(ambient, 1);
                return lerp(_BottomColor, _TopColor * lightColor, IN.uv.y);
            }
            ENDHLSL
        }
    }
}
