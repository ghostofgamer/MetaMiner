Shader "Custom/URP_SimpleBlurWithoutGrabPass" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}  // Текстура для размытия
        _BumpMap ("Normal Map", 2D) = "bump" {}    // Карта нормалей
        _Color ("Tint Color", Color) = (1,1,1,1)   // Основной цвет
        _BumpAmt ("Distortion", Range(0,128)) = 10 // Сила искажения
        _Size ("Blur Size", Range(0, 20)) = 1      // Радиус размытия
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 positionOS : POSITION; // Позиция вершины
                float2 uv : TEXCOORD0;        // UV-координаты
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION; // Позиция в пространстве экрана
                float2 uv : TEXCOORD0;           // Интерполированные UV-координаты
            };

            // Параметры
            sampler2D _MainTex;
            sampler2D _BumpMap;
            float4 _MainTex_TexelSize; // Размер пикселя в текстуре
            float4 _BumpMap_TexelSize;
            float _BumpAmt;
            float4 _Color;
            float _Size;

            // Вершинный шейдер
            Varyings vert(Attributes v) {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS); // Конвертация в пространство экрана
                o.uv = v.uv;
                return o;
            }

            // Фрагментный шейдер
            half4 frag(Varyings i) : SV_Target {
                half4 sum = half4(0, 0, 0, 0);

                // Размытие по горизонтали
                #define BLUR_H(weight, offset) tex2D(_MainTex, i.uv + float2(offset * _MainTex_TexelSize.x * _Size, 0)) * weight
                sum += BLUR_H(0.05, -4.0);
                sum += BLUR_H(0.09, -3.0);
                sum += BLUR_H(0.12, -2.0);
                sum += BLUR_H(0.15, -1.0);
                sum += BLUR_H(0.18,  0.0);
                sum += BLUR_H(0.15, +1.0);
                sum += BLUR_H(0.12, +2.0);
                sum += BLUR_H(0.09, +3.0);
                sum += BLUR_H(0.05, +4.0);

                // Размытие по вертикали
                #define BLUR_V(weight, offset) tex2D(_MainTex, i.uv + float2(0, offset * _MainTex_TexelSize.y * _Size)) * weight
                sum += BLUR_V(0.05, -4.0);
                sum += BLUR_V(0.09, -3.0);
                sum += BLUR_V(0.12, -2.0);
                sum += BLUR_V(0.15, -1.0);
                sum += BLUR_V(0.18,  0.0);
                sum += BLUR_V(0.15, +1.0);
                sum += BLUR_V(0.12, +2.0);
                sum += BLUR_V(0.09, +3.0);
                sum += BLUR_V(0.05, +4.0);

                // Применение искажения
                float2 bump = tex2D(_BumpMap, i.uv).rg * 2.0 - 1.0; // Вектор смещения
                float2 offset = bump * _BumpAmt * _MainTex_TexelSize.xy;
                sum += tex2D(_MainTex, i.uv + offset) * 0.5;

                // Тинтинг
                sum *= _Color;
                return sum;
            }
            ENDHLSL
        }
    }
}
