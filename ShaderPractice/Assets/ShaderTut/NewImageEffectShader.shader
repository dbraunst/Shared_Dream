Shader "Custom/NewImageEffectShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                // fixed4 col = tex2D(_MainTex, i.uv);
                // col.r = 1; //sets red
                // col.rgb = 1 - col.rgb; //Invert Colors

                //What if we offset things? by pixel? 
                // Texture coords range 0-1
                // fixed4 col = tex2D(_MainTex, i.uv + float2(0, 0.1);
                // ^^ moves all pixels up by 10%

                // What if we can adjust pixes proportionally? with a sin?
                // fixed4 col = tex2D(_MainTex, i.uv + float2(0, sin( i.vertex.x/200 ) / 10));

                // we can also add a time property to the above with the _Time[n]
                // _Time[] ==  {seconds / 20, seconds, seconds * 2, seconds * 3 };
                fixed4 col = tex2D(_MainTex, i.uv + float2(0, sin( i.vertex.x/200 + _Time[1]) / 40));
                //Also we have sin (<distortion> + <time scalar> ) / <sin height>

                // OK, how can we randomize this? No real 'random' exists in HLSL, but
                //   we can use a noise texture! <- that's for next episode
                

                return col;
            }
            ENDCG
        }
    }
}
