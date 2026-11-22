Shader "CustomRenderTexture/BorderShader"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _ColorA ("Color A", Color) = (0,1,1,1)
        _ColorB ("Color B", Color) = (1,0,1,1)
        _BorderWidth ("Border Width", Range(0, 0.3)) = 0.08
        _Waves ("Waves (integer)", Float) = 3
        _Speed ("Speed", Float) = 3
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Cull Off ZWrite Off ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float4 color : COLOR; float2 uv : TEXCOORD0; };
            struct v2f { float4 pos : SV_POSITION; fixed4 color : COLOR; float2 uv : TEXCOORD0; };

            fixed4 _Color, _ColorA, _ColorB;
            float _BorderWidth, _Waves, _Speed;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 p = i.uv - 0.5;
                float angle = atan2(p.y, p.x);

                // волна бежит по кругу вдоль рамки
                float wave = sin(angle * _Waves - _Time.y * _Speed) * 0.5 + 0.5;

                // расстояние до ближайшего края
                float2 d = min(i.uv, 1 - i.uv);
                float edge = min(d.x, d.y);

                // толщина рамки "дышит" вместе с волной
                float width = _BorderWidth * lerp(0.4, 1.0, wave);
                float a = 1 - smoothstep(width - 0.015, width, edge);

                fixed3 col = lerp(_ColorA.rgb, _ColorB.rgb, wave) * i.color.rgb;
                return fixed4(col, a * i.color.a);
            }
            ENDCG
        }
    }
}
