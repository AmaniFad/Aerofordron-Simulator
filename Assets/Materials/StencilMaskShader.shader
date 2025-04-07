Shader "Unlit/StencilMaskShader"
{
    
        Properties
        {
            _MainTex("Texture", 2D) = "black" {}
        }
            SubShader
        {
            Tags { "RenderType" = "Opaque" "Queue" = "Geometry+2" }

            Stencil
            {
                Ref 1         // Only render where stencil value is 1
                Comp Equal    // Only pass where stencil matches Ref
            }

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
                };

                struct Varyings
                {
                    float4 positionHCS : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);

                Varyings vert(Attributes IN)
                {
                    Varyings OUT;
                    OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                    OUT.uv = IN.uv;
                    return OUT;
                }

                half4 frag(Varyings IN) : SV_Target
                {
                    return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                }
                ENDHLSL
            }
        }

}
