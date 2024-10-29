Shader "WaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.5
        _WaveFrequency ("Wave Frequency", Float) = 1.0
    }

    SubShader
    {
        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;

            // Wave parameters
            uniform float _WaveSpeed;
            uniform float _WaveAmplitude;
            uniform float _WaveFrequency;

            struct vertIn
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertOut
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex shader implementation
            vertOut vert(vertIn v)
            {
                // Calculate wave displacement
                float wave = sin(v.vertex.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveAmplitude;
                v.vertex.y += wave;

                vertOut o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader implementation
            fixed4 frag(vertOut v) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, v.uv);
                return col;
            }
            ENDCG
        }
    }
}
