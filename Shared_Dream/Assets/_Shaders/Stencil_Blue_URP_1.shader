Shader "Universal Render Pipeline/Stencil_Blue_URP_1"
{
    Properties
    {
        [MainTexture] _BaseMap("Texture", 2D) = "white" {}
        [MainColor]   _BaseColor("Color", Color) = (0, 0.5, 1, 1)
        _Cutoff("AlphaCutout", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        
        Pass
        {
            Stencil {
                Ref 1
                Comp equal
                Pass replace
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                //position in Object Space
                float4 positionOS       : POSITION;
                float2 uv               : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float2 uv        : TEXCOORD0;
                float4 vertex : SV_POSITION;

                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float4 _BaseColor;

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                // VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                // output.vertex = vertexInput.positionCS;
                // output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                output.vertex = TransformObjectToHClip(input.positionOS.xyz);

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                // half2 uv = input.uv;
                // half4 texColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
                // half3 color = texColor.rgb * _BaseColor.rgb;
                // half alpha = texColor.a * _BaseColor.a;
                // AlphaDiscard(alpha, _Cutoff);

                half4 customColor = half4(0, 0.5, 1, 1);

                return half4(customColor);
            }
            ENDHLSL
        }
    }
}
