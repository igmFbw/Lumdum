Shader "Hidden/HandDrawn"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _HandDrawnAmount ("Hand Drawn Amount", Range(0, 20)) = 10
        _HandDrawnSpeed ("Hand Drawn Speed", Range(1, 15)) = 5
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off  // 关闭背面剔除
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _HandDrawnAmount, _HandDrawnSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            // 伪随机
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.y * _HandDrawnSpeed;
                
                // 生成手绘抖动
                float2 noiseUV = floor(uv * 50.0) + floor(time);
                float noiseX = (rand(noiseUV) - 0.5);
                float noiseY = (rand(noiseUV + 17.0) - 0.5);
                
                float2 offset = float2(noiseX, noiseY) * 0.003 * _HandDrawnAmount;
                float2 finalUV = uv + offset;
                
                // 限制范围
                finalUV = clamp(finalUV, 0.0, 1.0);
                
                fixed4 col = tex2D(_MainTex, finalUV);
                col *= i.color;
                
                return col;
            }
            ENDCG
        }
    }
    
    // 回退Shader
    FallBack "Sprites/Default"
}
