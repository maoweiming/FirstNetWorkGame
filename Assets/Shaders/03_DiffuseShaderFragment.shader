// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader"SSM/03_DiffuseShaderFragment"{

	Properties{

		_DiffuseColor("Color",Color) = (1,1,1,1)

	}

	SubShader{

		pass{

		Tags{"LightMode" = "ForwardBase"}

		CGPROGRAM

		#include "Lighting.cginc"

		#pragma vertex vert
		#pragma fragment frag

		fixed4 _DiffuseColor;

		struct a2v{
			float4 vertex:POSITION;
			float3 normal:NORMAL;
		};

		struct v2f{
			float4 position:SV_POSITION;
			fixed3 worldNormalDir :COLOR0;
		};
	
		v2f vert(a2v v){
			v2f f;

			f.position = UnityObjectToClipPos(v.vertex);

			f.worldNormalDir = mul(v.normal,(float3x3)unity_WorldToObject);

			return f;
		}

		fixed4 frag(v2f f):SV_TARGET{

			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;

			fixed3 normalDir = normalize(f.worldNormalDir);

			fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

			fixed3 diffuse = _LightColor0.rgb * max(0,dot(normalDir,lightDir)) * _DiffuseColor.rgb;

			fixed3 tempColor = diffuse + ambient;

			return fixed4(tempColor,1);
		}


		ENDCG
		}
	}

	Fallback"Diffuse"
}