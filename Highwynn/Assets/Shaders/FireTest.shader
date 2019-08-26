Shader "Fire/FireTest"
{
    Properties
    {

        _NoiseA ("Noise A", 2D) = "white" {}
		_NoiseB("Noise B", 2D) = "white" {}
		_ShapeMask("Shape Mask", 2D) = "white" {}
		_DistMask("Distortion Mask", 2D) = "white" {}
		_PrimarySpeed("Primary Speed", Range(0, 10)) = 1
		_SecondarySpeed("Primary Speed", Range(0, 10)) = 1
		_Clip("Clipping Threshold", Range(0, 1)) = 1
		_ClipHole("Jagged Edge Clipping Threshold", Range(0, 1)) = 1
		
		_DistStr("Distortion Strength", Range(0, 5)) = 1

		_DistStrA("Tear Strength", Range(0, 5)) = 1
		_DistStrB("Wobble Strength", Range(0, 1)) = 0
		_Speed("Wobble Speed", Range(0, 50)) = 0
		_DistMaskIgnore("Wobble Persistence", Range(0, 1)) = 0
		_ExtColour("External Colour", color) = (1, 1, 1, 1)
		_TransColour("Transition Colour", color) = (1, 1, 1, 1)
		_IntColour("Internal Colour", color) = (1, 1, 1, 1)

		

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
				float2 noiseAuv : TEXCOORD1;
				float2 noiseBuv : TEXCOORD2;
				float2 uvShape : TEXCOORD3;
                float4 vertex : SV_POSITION;
            };

			float4 _ExtColour;
			float4 _IntColour;
			float4 _TransColour;
			float _DistStr;
			float _DistStrA;
			float _DistStrB;
			float _Clip;
			float _ClipHole;
			float _PrimarySpeed;
			float _SecondarySpeed;
			float _Speed;
			float _DistMaskIgnore;
			sampler2D _ShapeMask;
			sampler2D _NoiseA;
			sampler2D _NoiseB;
			sampler2D _DistMask;
            float4 _NoiseA_ST;
			float4 _NoiseB_ST;
			float4 _DistMask_ST;
			float4 _ShapeMask_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _DistMask);
				o.uvShape = TRANSFORM_TEX(v.uv, _ShapeMask);
				o.noiseAuv = TRANSFORM_TEX(v.uv, _NoiseA);
				o.noiseBuv = TRANSFORM_TEX(v.uv, _NoiseB);               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                

				fixed4 mask = tex2D(_DistMask, i.uv);
				fixed4 distortionA = tex2D(_NoiseA, float2(i.noiseAuv.x, i.noiseAuv.y + -_Time.y * _PrimarySpeed));
				fixed4 distortionB = tex2D(_NoiseB, float2(i.noiseBuv.x, i.noiseBuv.y + -_Time.y * _SecondarySpeed));
				fixed4 distortionC = tex2D(_NoiseA, float2(i.noiseAuv.x, i.noiseAuv.y + -_Time.y * _PrimarySpeed));
				fixed4 col = distortionB - distortionA * _DistStrA;
				fixed4 shapeMask = tex2D(_DistMask, float2(i.uv.x + (sin((i.uv.y * 10 + (_Time.y * _Speed))) * _DistStrB * (mask.b + _DistMaskIgnore) * distortionA.r), i.uv.y + (col.r * _DistStr) * mask.b));

				col = tex2D(_ShapeMask, float2(i.uvShape.x + (sin((i.uvShape.y * 10 + (_Time.y * _Speed))) * _DistStrB * (mask.b + _DistMaskIgnore) * distortionA.r), i.uvShape.y + (col.r * _DistStr) * mask.b));

				clip(col.r - _Clip);
				col.g *= ((distortionA.r - 0.1) + (_ClipHole - mask.b)) * (-mask.g + 1);
				col.b *= (distortionA.r - 0.1) + (_ClipHole - mask.b);
				clip(col.r * (distortionC.r - 0.1) + ((-mask.b + 1) + _ClipHole));

				col = _ExtColour * smoothstep(0, 0.001, col.r) + _TransColour * smoothstep(0, 0.01, col.g) + _IntColour * smoothstep(0, 0.01, col.b);

								
                
                return col;
            }
            ENDCG
        }
    }
}
