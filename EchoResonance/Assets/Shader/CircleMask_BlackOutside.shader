Shader "Custom/CircleMask_BlackOutside"
{
    Properties
    {
        _Center ("Circle Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Range(0, 1)) = 0.3
        _EdgeSoftness ("Edge Softness", Range(0.001, 0.4)) = 0.15
        _BlackAlpha ("Black Area Alpha", Range(0,1)) = 0.9
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float2 _Center;
            float _Radius;
            float _EdgeSoftness;
            float _BlackAlpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float dist = length(uv - _Center);

                float circle = smoothstep(_Radius, _Radius + _EdgeSoftness, dist);

                fixed4 col;
                col.rgb = fixed3(0, 0, 0);
                col.a = circle * _BlackAlpha;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}