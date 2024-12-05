Shader "UI/Blur2DTransparentOptimizedWithIntensity"
{
    Properties
    {
        _MainTex("Custom Texture", 2D) = "white" {}
        _Radius("Blur Radius", Float) = 1
        _SampleCount("Number of Samples", Int) = 10
        _ColorIntensity("Color Intensity", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        HLSLINCLUDE

        sampler2D _MainTex;

        // Униформ-переменные
        uniform float4 _MainTex_TexelSize;
        uniform int _Radius;
        uniform int _SampleCount;
        uniform float _ColorIntensity;

        // Матрица модели-вида-проекции
        uniform float4x4 unity_ObjectToWorld;
        uniform float4x4 unity_MatrixVP;

        // Преобразование координат объекта в клип-пространство
        float4 TransformObjectToClipSpace(float4 positionOS)
        {
            float4 positionWS = mul(unity_ObjectToWorld, positionOS);
            return mul(unity_MatrixVP, positionWS);
        }

        // Псевдослучайная функция с небольшими смещениями для устранения артефактов
        float rand(float2 uv, int sampleIndex, bool isX)
        {
            // Добавляем случайное смещение с изменением на основе индекса сэмпла
            float2 randUV = uv + (isX ? float2(sampleIndex, 0.0) : float2(0.0, sampleIndex)) / _SampleCount;
            float combined = dot(randUV, float2(12.9898, 78.233));
            combined = frac(sin(combined) * 43758.5453);
            return combined;
        }

        struct appdata
        {
            float4 positionOS : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 positionCS : SV_Position;
            float2 uv : TEXCOORD0;
        };

        v2f vert(appdata v)
        {
            v2f o;
            o.positionCS = TransformObjectToClipSpace(v.positionOS); // Использует существующую функцию
            o.uv = v.uv;
            return o;
        }

        // Основная функция размытия с улучшенным сглаживанием
        float4 frag_blur2d(v2f i) : SV_Target
        {
            float4 col = float4(0.0f, 0.0f, 0.0f, 0.0f);
            float weightSum = 0.0f;

            int radius = max(_Radius, 1);
            int sampleCount = max(_SampleCount, 1);

            // Для сглаживания случайных значений
            for (int s = 0; s < sampleCount; ++s)
            {
                // Получаем смещения для осей X и Y с добавлением случайных изменений для уменьшения шума
                int xOffset = (int)(rand(i.uv, s, true) * radius * 2 - radius);
                int yOffset = (int)(rand(i.uv, s, false) * radius * 2 - radius);

                // Меньше диапазона для сглаживания
                float2 uv = i.uv + float2(_MainTex_TexelSize.x * xOffset * 0.5, _MainTex_TexelSize.y * yOffset * 0.5);
                
                // Гауссово распределение для более плавного уменьшения веса (сглаживание)
                float weight = exp(-(xOffset * xOffset + yOffset * yOffset) / (2.0 * radius * radius));
                
                // Добавляем пиксель с учетом веса
                col += tex2D(_MainTex, uv) * weight;
                weightSum += weight;
            }

            // Нормализуем и учитываем итоговое размытие
            col /= weightSum;
            col.rgb *= _ColorIntensity;

            return col;
        }

        ENDHLSL

        Pass
        {
            Name "Blur2DTransparentOptimizedWithIntensity"
            Tags { "RenderType"="Transparent" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag_blur2d
            ENDHLSL
        }
    }
}
