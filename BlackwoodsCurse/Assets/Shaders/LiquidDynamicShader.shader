Shader "Custom/LiquidFill"
{
    Properties
    {
        _FillAmount ("Fill Amount", Range(0,1)) = 0.5
        _LiquidColor ("Liquid Color", Color) = (1,0,0,1)
        _TopColor ("Surface Color", Color) = (1,1,1,1)
        _WobbleX ("Wobble X", Range(-1,1)) = 0.1
        _WobbleZ ("Wobble Z", Range(-1,1)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off // Important for seeing inside
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float fillPos : TEXCOORD1;
            };

            float _FillAmount;
            float4 _LiquidColor;
            float4 _TopColor;
            float _WobbleX;
            float _WobbleZ;

            v2f vert (appdata v)
            {
                v2f o;
                
                // Wobble effect
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 wobble = float3(
                    sin(_Time.y + worldPos.y) * _WobbleX,
                    0,
                    cos(_Time.y + worldPos.y) * _WobbleZ
                );
                v.vertex.xyz += wobble * v.normal;
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = worldPos;
                o.fillPos = v.vertex.y + wobble.y;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Clip liquid above fill line
                clip(_FillAmount - i.fillPos);
                
                // Surface detection
                float surface = smoothstep(0.02, 0.05, _FillAmount - i.fillPos);
                fixed4 col = lerp(_LiquidColor, _TopColor, surface);
                
                // Edge glow
                float edge = 1 - saturate(abs(_FillAmount - i.fillPos) * 20);
                col.rgb += edge * 0.5;
                
                return col;
            }
            ENDCG
        }
    }
}