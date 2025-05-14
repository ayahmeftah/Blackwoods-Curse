Shader "Custom/MirrorFlicker"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _FlickerStrength ("Flicker Strength", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _FlickerStrength;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float flicker = lerp(0.5, 1.0, _FlickerStrength);
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * flicker;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
