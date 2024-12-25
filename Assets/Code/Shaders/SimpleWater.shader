Shader "Custom/SimpleWater"
{
    Properties
    {
        _WaterColor ("Water Color", Color) = (0.1, 0.5, 0.8, 1)
        _FoamColor ("Foam Color", Color) = (1, 1, 1, 1)
        _MainTex ("Wave Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Range(0.1, 2.0)) = 0.3
        _FoamCutoff ("Foam Cutoff", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _WaterColor;
            float4 _FoamColor;
            float _WaveSpeed;
            float _FoamCutoff;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float t = _Time.y * _WaveSpeed;
                
                // Scrolling UVs in two directions
                o.uv = v.uv + float2(t * 0.1, t * 0.05);
                o.uv2 = v.uv + float2(-t * 0.05, t * 0.07);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 tex1 = tex2D(_MainTex, i.uv);
                float4 tex2 = tex2D(_MainTex, i.uv2);
                
                // Blend two scrolling textures for moving waves
                float wave = (tex1.r + tex2.r) * 0.5;

                // Foam effect at wave peaks
                float foam = step(_FoamCutoff, wave);
                float4 finalColor = lerp(_WaterColor, _FoamColor, foam);

                // Transparency fades with foam
                finalColor.a = lerp(0.6, 1.0, foam);
                
                return finalColor;
            }
            ENDCG
        }
    }
}