Shader "Custom/EdgeGlowShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _GlowStrength ("Glow Strength", Float) = 0.5
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
                float glowStrength : FLOAT;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                o.baseColor = _BaseColor; // Check if this line is correct
                o.glowColor = _GlowColor;
                o.glowStrength = _GlowStrength;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = i.baseColor; // Check if this line is correct

                // Calculate the distance from the edge
                float edgeDist = distance(i.uv, float2(0.5, 0.5));

                // Adjust the color based on the distance from the edge and glow strength
                color += i.glowColor * i.glowStrength * smoothstep(i.glowStrength, 0, edgeDist);

                return color;
            }
            ENDCG
        }
    }
}