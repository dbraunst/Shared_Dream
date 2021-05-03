Shader "Universal Render Pipeline/TimeWindow_1_URP"
{
    Properties
    {
        [MainTexture] _BaseMap("Camera Texture 0", 2D) = "white"
        // _CameraTex0 ("Camera Texture 0", 2D) = "gray" {}
        // _CameraTex1 ("Camera Texture 1", 2D) = "white" {}
        // _CameraTex2 ("Camera Texture 2", 2D) = "white" {}
        // _CameraTex3 ("Camera Texture 2", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" "RenderPipeline" = "UniversalPipeline"}
        LOD 100

        // Layer 5, Camera 0
        Pass
        {
            Name "Main Camera"
            // Stencil {
            //     Ref 2
            //     Comp Equal
            //     Pass replace
            // }

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
                float4 vertex : SV_POSITION;
                float2 uv        : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Texture2D _BaseMap;
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                // VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                // output.vertex = vertexInput.positionCS;
                output.vertex = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);

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

                // half4 customColor = half4(0, 0.5, 1, 1);
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);


                return color;
            }
            ENDHLSL
        }

        // Layer 6, Camera 1
        // Pass
        // {
        //     Name "Red Camera"
        //     Stencil {
        //         Ref 6
        //         Comp Equal
        //         Pass keep
        //     }

        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag
        //     // make fog work
        //     #pragma multi_compile_fog

        //     #include "UnityCG.cginc"

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float2 uv : TEXCOORD0;
        //     };

        //     struct v2f
        //     {
        //         float2 uv : TEXCOORD0;
        //         UNITY_FOG_COORDS(1)
        //         float4 vertex : SV_POSITION;
        //     };

        //     sampler2D _MainTex;
        //     float4 _MainTex_ST;

        //     sampler2D _CameraTex1;

        //     sampler2D _Cam1RT;

        //     v2f vert (appdata v)
        //     {
        //         v2f o;
        //         o.vertex = UnityObjectToClipPos(v.vertex);
        //         o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //         UNITY_TRANSFER_FOG(o,o.vertex);
        //         return o;
        //     }

        //     fixed4 frag (v2f i) : SV_Target
        //     {
        //         // sample the texture
        //         fixed4 col = tex2D(_CameraTex1, i.uv);
        //         // apply fog
        //         UNITY_APPLY_FOG(i.fogCoord, col);
        //         return col;
        //     }
        //     ENDCG
        // }

        // // Layer 7, Camera 2
        // Pass
        // {
        //     Name "Green Camera"
        //     Stencil {
        //         Ref 7
        //         Comp Equal
        //         Pass keep
        //     }

        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag
        //     // make fog work
        //     #pragma multi_compile_fog

        //     #include "UnityCG.cginc"

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float2 uv : TEXCOORD0;
        //     };

        //     struct v2f
        //     {
        //         float2 uv : TEXCOORD0;
        //         UNITY_FOG_COORDS(1)
        //         float4 vertex : SV_POSITION;
        //     };

        //     sampler2D _MainTex;
        //     float4 _MainTex_ST;

        //     sampler2D _CameraTex2;

        //     v2f vert (appdata v)
        //     {
        //         v2f o;
        //         o.vertex = UnityObjectToClipPos(v.vertex);
        //         o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //         UNITY_TRANSFER_FOG(o,o.vertex);
        //         return o;
        //     }

        //     fixed4 frag (v2f i) : SV_Target
        //     {
        //         // sample the texture
        //         fixed4 col = tex2D(_CameraTex2, i.uv);
        //         // apply fog
        //         UNITY_APPLY_FOG(i.fogCoord, col);
        //         return col;
        //     }
        //     ENDCG
        // }

        // // Layer 8, Camera 3
        // Pass
        // {
        //     Stencil {
        //         Ref 8
        //         Comp Equal
        //         Pass keep
        //     }

        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag
        //     // make fog work
        //     #pragma multi_compile_fog

        //     #include "UnityCG.cginc"

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float2 uv : TEXCOORD0;
        //     };

        //     struct v2f
        //     {
        //         float2 uv : TEXCOORD0;
        //         UNITY_FOG_COORDS(1)
        //         float4 vertex : SV_POSITION;
        //     };

        //     sampler2D _MainTex;
        //     float4 _MainTex_ST;

        //     sampler2D _CameraTex3;

        //     v2f vert (appdata v)
        //     {
        //         v2f o;
        //         o.vertex = UnityObjectToClipPos(v.vertex);
        //         o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //         UNITY_TRANSFER_FOG(o,o.vertex);
        //         return o;
        //     }

        //     fixed4 frag (v2f i) : SV_Target
        //     {
        //         // sample the texture
        //         fixed4 col = tex2D(_Cam2RT, i.uv);
        //         // apply fog
        //         UNITY_APPLY_FOG(i.fogCoord, col);
        //         return col;
        //     }
        //     ENDCG
        // }
    }
}
