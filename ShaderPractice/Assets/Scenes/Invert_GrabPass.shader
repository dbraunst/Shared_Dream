Shader "Stencil/Invert"
{
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent"}
        // LOD 100

        GrabPass {
            "_BackgroundTexture"
        }

        Pass
        {
            // Stencil {
            //     Ref 6
            //     Comp equal
            // }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            // #pragma multi_compile_fog

            #include "UnityCG.cginc"

            // struct appdata
            // {
            //     float4 vertex : POSITION;
            //     float2 uv : TEXCOORD0;
            // };

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                // UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            sampler2D _BackgroundTexture;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                // return half4(0, 0, 1, 1);
                half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
                return 1 - bgcolor;
            }
            ENDCG
        }
    }
}
