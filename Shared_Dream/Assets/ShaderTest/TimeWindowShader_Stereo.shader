Shader "UI/TimeWindowShader_Stereo"
{
    // Thanks: https://www.weshipcode.com/developers/vr-optimizations-in-unity-single-pass-stereo/

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CameraTex0 ("Camera Texture 0", 2D) = "gray" {}
        _CameraTex1 ("Camera Texture 1", 2D) = "white" {}
        _CameraTex2 ("Camera Texture 2", 2D) = "white" {}
        // _CameraTex3 ("Camera Texture 2", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+50"}
        LOD 100

        // Layer 5, Camera 0
        Pass
        {
            Name "Main Camera"
            Stencil {
                Ref 5
                Comp Equal
                Pass keep
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

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

            sampler2D _CameraTex0;
            float4 _CameraTex0_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _CameraTex0);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_CameraTex0, i.uv);
                fixed4 col = tex2D(_CameraTex0, UnityStereoScreenSpaceUVAdjust(i.uv, _CameraTex0_ST));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

        // Layer 6, Camera 1
        Pass
        {
            Name "Red Camera"
            Stencil {
                Ref 6
                Comp Equal
                Pass keep
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                // float2 uv : TEXCOORD0;
                // UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _CameraTex1;

            sampler2D _Cam1RT;

            // // Tranforms position from object to homogenous space
            // inline float4 UnityObjectToClipPos( in float3 pos )
            // {
            // #if defined(UNITY_SINGLE_PASS_STEREO) || defined(UNITY_USE_CONCATENATED_MATRICES)
            //     // More efficient than computing M*VP matrix product
            //     return mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, float4(pos, 1.0)));
            // #else
            //     return mul(UNITY_MATRIX_MVP, float4(pos, 1.0));
            // #endif
            // }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                i.screenPos /= i.screenPos.w;
                fixed4 col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y));

                // fixed4 col = tex2D(_CameraTex1, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

        // Layer 7, Camera 2
        Pass
        {
            Name "Green Camera"
            Stencil {
                Ref 7
                Comp Equal
                Pass keep
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _CameraTex2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_CameraTex2, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

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
