Shader "Custom/PulsingEdgeGlowShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _MinGlowStrength ("Minimum Glow Strength", Float) = 0.1
        _MaxGlowStrength ("Maximum Glow Strength", Float) = 0.5
        _PulseFrequency ("Pulse Frequency", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 baseColor : COLOR;
                float4 glowColor : COLOR;
                float minGlowStrength : FLOAT;
                float maxGlowStrength : FLOAT;
                float pulseFrequency : FLOAT;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                o.baseColor = _BaseColor;
                o.glowColor = _GlowColor;
                o.minGlowStrength = _MinGlowStrength;
                o.maxGlowStrength = _MaxGlowStrength;
                o.pulseFrequency = _PulseFrequency;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = i.baseColor;

                // Calculate the distance from the edge
                float edgeDist = distance(i.uv, float2(0.5, 0.5));

                // Calculate the pulsating glow strength
                float pulse = sin(_Time.y * i.pulseFrequency);
                float glowStrength = lerp(i.minGlowStrength, i.maxGlowStrength, pulse * 0.5 + 0.5);

                // Adjust the color based on the distance from the edge and glow strength
                color += i.glowColor * glowStrength * smoothstep(i.glowStrength, 0, edgeDist);

                return color;
            }
            ENDCG
        }
    }
}