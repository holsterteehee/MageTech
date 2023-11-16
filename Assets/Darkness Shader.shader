Shader "Custom/DarknessShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o) {
            // Calculate darkness based on distance from light source
            float darkness = 1 - length(IN.uv_MainTex - _LightPos.xy) / _LightRadius;

            // Apply darkness effect
            o.Albedo = o.Albedo * darkness * _Color.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}