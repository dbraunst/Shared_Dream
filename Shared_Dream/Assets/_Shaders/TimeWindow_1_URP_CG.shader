// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Universal Render Pipeline/TimeWindow_1_URP_CG"
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
            // Name "Main Camera"
            // Stencil {
            //     Ref 2
            //     Comp Equal
            //     Pass replace
            // }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.5

            // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "UnityCG.cginc"

            struct appData
            {
                //position in Object Space
                float4 pos       : POSITION;
                float2 uv        : TEXCOORD0;
            };

            struct v2f
            {
                
                float4 vertex : SV_POSITION;
                float2 uv        : TEXCOORD0;
                float3 screen_uv : TEXCOORD1;
                // UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            // UNITY_DECLARE_TEX2DARRAY(_BaseMap);
            sampler2D _BaseMap;
            float4 _BaseMap_ST;

            v2f vert(appData v)
            {
                v2f o;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.pos);
                o.uv = v.uv;
                o.screen_uv = float3(o.vertex.xy, o.vertex.w);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                float2 corrected_UV;
                corrected_UV[0] = (i.screen_uv.x / i.screen_uv.z + 1);
                corrected_UV[1] = (1 - i.screen_uv.y / i.screen_uv.z);


                //fixed4 col = tex2D(_BaseMap, i.uv);
                return tex2D(_BaseMap, corrected_UV);
            }
            ENDCG
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
