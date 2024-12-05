Shader "Custom/UI Blur Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Float) = 1.0
        _Quality ("Quality", Float) = 64.0
        _Color ("Color", Color) = (1,1,1,1)
        _Intensity ("Color Intensity", Float) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurAmount;
            float _Quality;
            fixed4 _Color;
            float _Intensity;

            // Улучшенная случайная функция
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            float random(float2 uv, float seed)
            {
                return frac(sin(dot(uv.xy * seed, float2(127.1, 311.7))) * 43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float4 col = float4(0, 0, 0, 0);
                int counter = 0;

                for (int x = -_BlurAmount; x <= _BlurAmount; x++)
                {
                    for (int y = -_BlurAmount; y <= _BlurAmount; y++)
                    {
                        if (x * x + y * y <= _BlurAmount * _BlurAmount) // Circle check
                        {
                            // Случайное смещение
                            float2 offset = float2(
                                rand(uv),
                                rand(uv)
                            ) - 0.5;

                            col += tex2D(_MainTex, uv + (float2(x, y) + offset) / _Quality).rgba;
                            counter++;
                        }
                    }
                }
                col /= counter;
                col *= _Color;
                col.rgb *= _Intensity; 
                //col.a = tex2D(_MainTex, uv).a; // Preserve alpha channel
                return col;
            }

            ENDCG
        }
    }
}
