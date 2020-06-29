Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RedValue ("Red Value", Float) = 0.5 
        _GreenValue ("Green Value", Float) = 0.5 
        _BlueValue ("Blue Value", Float) = 0.5 
        _LerpValue ("Lerp Value", Float) = 0.5
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _RedValue;
            float _GreenValue;
            float _BlueValue;
            float _LerpValue;

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

           fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
            
                // float redPerc = _RedValue;
                // float greenPerc = _GreenValue;
                // float bluePerc = _BlueValue;

                float redPerc = .6;
                float greenPerc = .4;
                float bluePerc = .0;

                float redPercLerp = .1;
                float greenPercLerp = .6;
                float bluePercLerp = 1.0;


                float lerpValue =_LerpValue;
                float2 uv = 2.0 * i.uv;


                for(int n = 1; n < 4; n++) {
                    float i = float(n);
                    uv += float2(3.4 / i *sin(i * uv.y + _Time.y + 1.2 * i) + 2., 
                        .3 / i * sin(uv.x + _Time.y + 1. * i) + 1.0);
                }
                
                float3 color = float3(0.1 * sin(uv.x) + redPerc, 
                    0.1* sin(uv.y) + greenPerc, 
                    0.1 * sin(uv.x + uv.y) + bluePerc);
                float3 colorLerp = float3(0.1 * sin(uv.x) + redPercLerp, 
                    0.1* sin(uv.y) + greenPercLerp, 
                    0.1 * sin(uv.x + uv.y) + bluePercLerp);

                color = lerp(color, colorLerp, lerpValue);

                return float4(color, 1.0);
            }

 
            ENDCG
        }
    }
}
